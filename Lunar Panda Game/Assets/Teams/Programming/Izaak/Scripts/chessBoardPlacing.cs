using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoardPlacing : MonoBehaviour
{
    public GameObject boardSquare;
    private GameObject instBoardSquare;
    public Vector2 pawnCorrectSpot;
    public Vector2 queenCorrectSpot;

    public struct chessCharacteristics
    {
        public Vector2 pieceLocation;
        public string pieceName;
    }

    public List<chessCharacteristics> chessPieces;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                instBoardSquare = (GameObject)Instantiate(boardSquare, transform.position, transform.rotation);
                instBoardSquare.transform.parent = gameObject.transform;
                instBoardSquare.GetComponent<chessPuzzle>().moveToCorrectPosition(new Vector2(i, j), 0.52f);
            }
        }
        createChessPieces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createChessPieces()
    {
        for(int i = 0; i < chessPieces.Count; i++)
        {

        }
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
