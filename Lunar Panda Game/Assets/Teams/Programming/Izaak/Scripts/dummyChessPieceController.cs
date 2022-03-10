using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyChessPieceController : MonoBehaviour
{
    [Tooltip("Name of the piece on the board. Use it just so you know what piece this is. Doesn't actually do anything")]
    public string pieceName;
    [Tooltip("The board co-ordinates where this should be stored. Do not put one where one of the item pieces needs to go")]
    public Vector2 boardPosition;
    [Tooltip("The material of the object, can be used to make black and white pieces")]
    public Material pieceMaterial;
    [Tooltip("The mesh that gives this object its form")]
    public Mesh objectMesh;
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
