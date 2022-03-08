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
        moveToCorrectPosition(boardPosition, 0.52f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLocation(Vector2 grid)
    {
        boardPosition = grid;
    }

    public void moveToCorrectPosition(Vector2 aBoardPosition, float yValue)
    {
        const float locationMult = 0.125f;
        const float offset = -0.4375f;
        transform.localPosition = new Vector3(offset + (aBoardPosition.x * locationMult), yValue, offset + (aBoardPosition.y * locationMult));
    }
}
