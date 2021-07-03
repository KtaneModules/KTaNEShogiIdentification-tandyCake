using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class ShogiIdentificationScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMBombModule Module;

    public KMSelectable SymbolButton;
    public KMSelectable[] GridButtons;
    public GameObject ModObj;
    public TextMesh symbolText;
    public MeshRenderer pieceMat;

    static readonly string[] coordinates = Enumerable.Range(0, 25).Select(x => "" + "ABCDE"[x % 5] + "12345"[x / 5]).ToArray();
    private Piece chosenPiece;
    private int[] solution;
    private bool[] pressed = new bool[25];
    int pressedCount;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    bool showingGrid;
    bool animating;

    void Awake () {
        moduleId = moduleIdCounter++;
        for (int i = 0; i < 25; i++)
        {
            int ix = i;
            GridButtons[ix].OnInteract += delegate () { ButtonPress(ix); return false; };
        }
        SymbolButton.OnInteract += delegate () { StartPress(); return false; };
        pieceMat.material.SetTextureOffset("_MainTex", new Vector2(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
    }

    void Start()
    {
        chosenPiece = Piece.allPieces.PickRandom();
        solution = chosenPiece.possibleMoves.Cast<int>().ToArray();
        symbolText.text = chosenPiece.symbol.ToString();
        Debug.LogFormat("[Shogi Identification #{0}] The displayed piece is {1} ({2}).", moduleId, chosenPiece.name, chosenPiece.symbol);
        Debug.LogFormat("[Shogi Identification #{0}] You should press the tiles {1} relative to the center square.", moduleId, chosenPiece.possibleMoves.Join(" "));

    }

    void StartPress()
    {
        if (showingGrid || animating)
            return;
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, SymbolButton.transform);
        if (!moduleSolved)
            StartCoroutine(Flip());
    }

    void ButtonPress(int pos)
    {
        if (!showingGrid || animating)
            return;
        GridButtons[pos].AddInteractionPunch(0.25f);
        if (!pressed[pos])
            Audio.PlaySoundAtTransform("tick", GridButtons[pos].transform);
        if (moduleSolved || pos == 12) //The middle square just won't do anything. You don't need to press it either
            return;
        if (solution.Contains(pos))
        {
            if (!pressed[pos])
            {
                pressed[pos] = true;
                pressedCount++;
                StartCoroutine(MoveButton(pos));
                if (pressedCount == chosenPiece.possibleMoves.Count)
                    Solve();
            }
        }
        else
        {
            Debug.LogFormat("[Shogi Identification #{0}] Pressed button {1}. Strike!.", moduleId, ((Move)pos).ToString());
            StartCoroutine(Strike());
        }
    }

    void Solve()
    {
        moduleSolved = true;
        Debug.LogFormat("[Shogi Identification #{0}] Module solved!", moduleId);
        StartCoroutine(Flip());
        Module.HandlePass();
        symbolText.text = "佳";
        symbolText.color = Color.green;
    }
    IEnumerator Strike()
    {
        pressed = new bool[25];
        pressedCount = 0;
        Module.HandleStrike();
        StartCoroutine(Flip());
        Start();
        yield return new WaitUntil(() => !animating);
        for (int i = 0; i < 25; i++)
            GridButtons[i].transform.localPosition = new Vector3(GridButtons[i].transform.localPosition.x, 14, GridButtons[i].transform.localPosition.z);
    }

    IEnumerator MoveButton(int pos)
    {
        Transform tf = GridButtons[pos].transform;
        float delta = 0;
        while (delta < 1)
        {
            delta += 4*Time.deltaTime;
            tf.localPosition = new Vector3(tf.localPosition.x, Mathf.Lerp(14, 18, delta), tf.localPosition.z);
            yield return null;
        }
    }

    IEnumerator Flip()
    {
        Audio.PlaySoundAtTransform("flickflickflick", transform);
        showingGrid = !showingGrid;
        animating = true;
        float from = showingGrid ? 0 : 180;
        float to = showingGrid ? 180 : 360;
        float duration = 1.5f;
        float delta = 0;
        while (delta < duration)
        {
            delta += Time.deltaTime;
            ModObj.transform.localRotation = Quaternion.Euler(Easing.OutSine(delta, from, to, duration), 0, 0);
            yield return null;
        }
        animating = false;
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} flip to flip the module over. Use !{0} press A1 B4 E5 to press those coordinates on the module. Letters represent columns from left to right, while numbers are rows from top to bottom.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string command)
    {
        command = command.Trim().ToUpperInvariant();
        List<string> parameters = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        if (command == "FLIP" || command == "START" && !showingGrid)
        {
            yield return null;
            SymbolButton.OnInteract();
            yield break;
        }
        if (parameters[0] != "PRESS")
            yield break;
        parameters.RemoveAt(0);
        if (parameters.All(x => coordinates.Contains(x)) && showingGrid)
            foreach (string coord in parameters)
            {
                yield return null;
                GridButtons[Array.IndexOf(coordinates, coord)].OnInteract();
                yield return new WaitForSeconds(0.15f);
            }
    }

    IEnumerator TwitchHandleForcedSolve ()
    {
        while (animating)
            yield return true;
        if (!showingGrid)
        {
            SymbolButton.OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        while (animating)
            yield return true;
        for (int i = 0; i < 25; i++)
            if (!pressed[i] && solution.Contains(i))
            {
                GridButtons[i].OnInteract();
                yield return new WaitForSeconds(0.15f);
            }
    }
}
