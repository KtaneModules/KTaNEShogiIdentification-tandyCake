using System.Collections.Generic;

public class Piece
{
    public string name { get; private set; }
    public char symbol { get; private set; }
    public List<Move> possibleMoves;
    
    public Piece(string pieceName, char sym, List<Move> moves)
    {
        name = pieceName;
        symbol = sym;
        possibleMoves = moves;
    }

    public static readonly Piece[] allPieces = new Piece[]
    {
        new Piece("Go-Between", '仲', new List<Move>(){Move.U, Move.D }),
        new Piece("Pawn", '歩', new List<Move>(){Move.U }),
        new Piece("Side Mover", '横', new List<Move>(){Move.U, Move.LL, Move.L, Move.R, Move.RR, Move.D}),
        new Piece("Vertical Mover", '竪', new List<Move>(){Move.UU, Move.U, Move.L, Move.R, Move.D, Move.DD}),
        new Piece("Bishop", '角', new List<Move>(){Move.UULL, Move.UURR, Move.UL, Move.UR, Move.DL, Move.DR, Move.DDLL, Move.DDRR}),
        new Piece("Rook", '飛', new List<Move>(){Move.UU, Move.U, Move.LL, Move.L, Move.R, Move.RR, Move.D, Move.DD }),
        new Piece("Dragon Horse", '馬', new List<Move>(){Move.UULL, Move.UURR, Move.UL, Move.U, Move.UR, Move.L, Move.R, Move.DL, Move.D, Move.DR, Move.DDLL, Move.DDRR}),
        new Piece("Dragon King", '龍', new List<Move>(){Move.UU, Move.UL, Move.U, Move.UR, Move.LL, Move.L, Move.R, Move.RR, Move.DL, Move.D, Move.DR, Move.DD }),
        new Piece("Lance", '香', new List<Move>(){Move.UU, Move.U }),
        new Piece("Reverse Chariot", '反', new List<Move>(){Move.UU, Move.U, Move.D, Move.DD }),
        new Piece("Blind Tiger", '虎', new List<Move>(){ Move.UL, Move.UR, Move.L, Move.R, Move.DL, Move.D, Move.DR}),
        new Piece("Ferocious Leopard", '豹', new List<Move>(){Move.UL, Move.U, Move.UR, Move.DL, Move.D, Move.DR}),
        new Piece("Copper General", '銅', new List<Move>(){Move.UL, Move.U, Move.UR, Move.D }),
        new Piece("Silver General", '銀', new List<Move>(){Move.UL, Move.U, Move.UR, Move.DL, Move.DR }),
        new Piece("Gold General", '金', new List<Move>(){Move.UL, Move.U, Move.UR, Move.L, Move.R, Move.D}),
        new Piece("Drunk Elephant", '象', new List<Move>(){Move.UL, Move.U, Move.UR, Move.L, Move.R, Move.DL, Move.DR}),
        new Piece("Kirin", '麒', new List<Move>(){Move.UU, Move.UL, Move.UR, Move.LL, Move.RR, Move.DL, Move.DR, Move.DD}),
        new Piece("Phoenix", '鳳', new List<Move>(){Move.UULL, Move.UURR, Move.U, Move.L, Move.R, Move.D, Move.DDLL, Move.DDRR}),
        new Piece("Queen", '奔', new List<Move>(){Move.UULL, Move.UU, Move.UURR, Move.UL, Move.U, Move.UR, Move.LL, Move.L, Move.R, Move.RR, Move.DL, Move.D, Move.DR, Move.DDLL, Move.DD, Move.DDRR }),
        new Piece("Flying Stag", '鹿', new List<Move>(){Move.UU, Move.UL, Move.U, Move.UR, Move.L, Move.R, Move.DL, Move.D, Move.DR, Move.DD }),
        new Piece("Flying Ox", '牛', new List<Move>(){Move.UULL, Move.UU, Move.UURR, Move.UL, Move.U, Move.UR, Move.DL, Move.D, Move.DR, Move.DDLL, Move.DD, Move.DDRR }),
        new Piece("Free Boar", '猪', new List<Move>(){Move.UULL, Move.UURR, Move.UL, Move.UR, Move.LL, Move.L, Move.R, Move.RR, Move.DL, Move.DR, Move.DDLL, Move.DDRR }),
        new Piece("Whale", '鯨', new List<Move>(){Move.UU, Move.U, Move.DL, Move.D, Move.DR, Move.DDLL, Move.DD, Move.DDRR }),
        new Piece("White Horse", '駒', new List<Move>(){Move.UULL, Move.UU, Move.UURR, Move.UL, Move.U, Move.UR, Move.D, Move.DD }),
        new Piece("King", '王', new List<Move>(){Move.UL, Move.U, Move.UR, Move.L, Move.R, Move.DL, Move.D, Move.DR }),
        new Piece("Prince", '王', new List<Move>(){Move.UL, Move.U, Move.UR, Move.L, Move.R, Move.DL, Move.D, Move.DR }),
        new Piece("Horned Falcon", '鷹', new List<Move>(){Move.UULL, Move.UU, Move.UURR, Move.UL, Move.U, Move.UR, Move.LL, Move.L, Move.R, Move.RR, Move.DL, Move.D, Move.DR, Move.DDLL, Move.DD, Move.DDRR }),
        new Piece("Soaring Eagle", '鷲', new List<Move>(){Move.UULL, Move.UU, Move.UURR, Move.UL, Move.U, Move.UR, Move.LL, Move.L, Move.R, Move.RR, Move.DL, Move.D, Move.DR, Move.DDLL, Move.DD, Move.DDRR }),
        new Piece("Lion", '獅', new List<Move>(){Move.UULL, Move.UUL, Move.UU, Move.UUR, Move.UURR, Move.ULL, Move.UL, Move.U, Move.UR, Move.URR, Move.LL, Move.L, Move.R, Move.RR, Move.DLL, Move.DL, Move.D, Move.DR, Move.DRR, Move.DDLL, Move.DDL, Move.DD, Move.DDR, Move.DDRR})
    };
}
