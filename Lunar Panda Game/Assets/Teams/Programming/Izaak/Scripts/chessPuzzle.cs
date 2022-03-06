using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessPuzzle : MonoBehaviour
{
    public Vector2 correctSpot;
    public GameObject chessboardParent;
    public GameObject queenChessPiece;
    public GameObject pawnChessPiece;
    public GameObject drawer;

    public List<GameObject> correctPieces;

    public bool puzzleComplete;

    private GameObject player;
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        //moveToCorrectPosition(correctSpot);
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (player.GetComponent<PlayerPickup>().heldItem == pawnChessPiece)
                    if (player.GetComponent<PlayerPickup>().heldItem == queenChessPiece)
                    {
                        
                        player.GetComponent<PlayerPickup>().DropHeldItem();
                        queenChessPiece.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                        queenChessPiece.GetComponent<HoldableItem>().enabled = false;
                        queenChessPiece.GetComponent<GlowWhenLookedAt>().enabled = false;
                        drawer.GetComponent<openChessDrawer>().puzzleCleared = true;
                    }
                }
            }
        }
    }

    public void placeChessPiece(GameObject piece)
    {
        player.GetComponent<PlayerPickup>().DropHeldItem();
        piece.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        if ((piece == queenChessPiece) || (piece == pawnChessPiece))
        {
            checkIfComplete(piece);
        }
    }

    public void checkIfComplete(GameObject piece)
    {
        if ((chessboardParent.GetComponent<chessBoardPlacing>().getQueenSpot() == correctSpot) || (chessboardParent.GetComponent<chessBoardPlacing>().getPawnSpot() == correctSpot))
        {
            correctPieces.Add(piece);
            checkPuzzleCompletion();
        }
    }

    public void checkPuzzleCompletion()
    {
        if (correctPieces.Count == 2)
        {
            drawer.GetComponent<openChessDrawer>().puzzleCleared = true;
            puzzleComplete = true;
        }
    }

    public void moveToCorrectPosition(Vector2 aBoardPosition, float yValue)
    {
        const float locationMult = 0.125f;
        const float offset = -0.4375f;
        transform.localPosition = new Vector3(offset + (aBoardPosition.x * locationMult), yValue, offset + (aBoardPosition.y * locationMult));
        Debug.Log(transform.position + "     " + transform.localPosition);
        Debug.Log(transform.parent);
    }
}
