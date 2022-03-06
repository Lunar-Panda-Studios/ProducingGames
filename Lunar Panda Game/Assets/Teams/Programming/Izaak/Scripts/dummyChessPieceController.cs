using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyChessPieceController : MonoBehaviour
{
    public string pieceName;
    public Vector2 boardPosition;

    public chessPuzzle chessScript;
    // Start is called before the first frame update
    void Start()
    {
        chessScript = FindObjectOfType<chessPuzzle>();
        chessScript.moveToCorrectPosition(boardPosition, 0.52f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
