using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoardPlacing : MonoBehaviour
{
    [Header("Board Co-ordinates")]
    [Tooltip("The correct co-ordinates for the pawn on the board")]
    public Vector2 pawnCorrectSpot;
    [Tooltip("The correct co-ordinates for the queen on the board")]
    public Vector2 queenCorrectSpot;
    [Header("Game Objects")]
    [Tooltip("Drag the prefab board square here")]
    public GameObject boardSquare;
    private GameObject instBoardSquare;
    [Tooltip("Drag the item pawn in the scene here")]
    public GameObject pawnPiece;
    [Tooltip("Drag the item queen in the scene here")]
    public GameObject queenPiece;
    [Tooltip("Drag the drawer in the scene here")]
    public GameObject drawer;

    private bool puzzleComplete = false;
    public bool check = false;

    public GameObject chessPieceObj;
    private GameObject instChessPiece;
    [System.Serializable]
    public struct chessCharacteristics
    {
        public Vector2 pieceLocation;
        public string pieceName;
        public Material pieceMaterial;
        public Mesh pieceMesh;
    }

    public List<chessCharacteristics> chessPieces;
    // Start is called before the first frame update
    void Start()
    {
        createChessSpots();
        createChessPieces();
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            checkPuzzleCompletion();
        }
    }

    public void createChessSpots()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                instBoardSquare = (GameObject)Instantiate(boardSquare, transform.position, transform.rotation);
                instBoardSquare.transform.parent = gameObject.transform;
                instBoardSquare.GetComponent<chessPuzzle>().moveToCorrectPosition(new Vector2(i, j), 0.52f);
                instBoardSquare.GetComponent<chessPuzzle>().queenChessPiece = queenPiece;
                instBoardSquare.GetComponent<chessPuzzle>().pawnChessPiece = pawnPiece;
                instBoardSquare.GetComponent<chessPuzzle>().chessboardParent = gameObject;
                for (int k = 0; k < chessPieces.Count; k++)
                {
                    if (chessPieces[k].pieceLocation == new Vector2(i,j))
                    {
                        instBoardSquare.GetComponent<chessPuzzle>().toggleOccupied();
                    }
                }
            }
        }
    }

    public void createChessPieces()
    {
        for(int i = 0; i < chessPieces.Count; i++)
        {
            instChessPiece = (GameObject)Instantiate(chessPieceObj, transform.position, transform.rotation);
            instChessPiece.transform.parent = gameObject.transform;
            instChessPiece.GetComponent<dummyChessPieceController>().setLocation(chessPieces[i].pieceLocation);
            instChessPiece.GetComponent<MeshRenderer>().material = chessPieces[i].pieceMaterial;
            instChessPiece.GetComponent<MeshFilter>().mesh = chessPieces[i].pieceMesh;
        }
    }

    public bool checkPuzzleCompletion()
    {
        check = false;
        Debug.Log(queenPiece.GetComponent<chessValuedItem>().checkCorrectSpot() + " " + pawnPiece.GetComponent<chessValuedItem>().checkCorrectSpot());
        if (queenPiece.GetComponent<chessValuedItem>().checkCorrectSpot() && pawnPiece.GetComponent<chessValuedItem>().checkCorrectSpot())
        {
            drawer.GetComponent<openChessDrawer>().puzzleCleared = true;
            puzzleComplete = true;
            return true;
        }
        return false;
    }

    public Vector2 getPawnSpot()
    {
        return pawnCorrectSpot;
    }

    public Vector2 getQueenSpot()
    {
        return queenCorrectSpot;
    }
}
