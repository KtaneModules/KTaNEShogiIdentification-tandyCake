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

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    bool showingGrid;

    void Awake () {
        moduleId = moduleIdCounter++;
        /*
        foreach (KMSelectable button in Buttons) 
            button.OnInteract += delegate () { ButtonPress(button); return false; };
        */

        //Button.OnInteract += delegate () { ButtonPress(); return false; };
        SymbolButton.OnInteract += delegate () { if (!showingGrid) StartCoroutine(Flip()); return false; };
    }

    void Start ()
    {

    }

    IEnumerator Flip()
    {
        showingGrid = !showingGrid;
        while (true)
        {
            ModObj.transform.Rotate(-100 * Time.deltaTime, 0, 0);
            yield return null;
        }
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
    }

    IEnumerator TwitchHandleForcedSolve () {
      yield return null;
    }
}
