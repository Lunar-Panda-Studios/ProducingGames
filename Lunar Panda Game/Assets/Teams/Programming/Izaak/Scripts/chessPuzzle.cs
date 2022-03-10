using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessPuzzle : MonoBehaviour
{
    [Header("Board Co-ordinates")]
    [Tooltip("The spot that this square occupies")]
    public Vector2 currentSpot;
    [Header("Game Objects")]
    [Tooltip("Drag the chessboard in the scene here")]
    public GameObject chessboardParent;
    [Tooltip("Drag the item queen here")]
    public GameObject queenChessPiece;
    [Tooltip("Drag the item pawn here")]
    public GameObject pawnChessPiece;

    private bool setOccupied = false;

    [HideInInspector]
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
            if (!setOccupied)
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        //player.GetComponent<PlayerPickup>().DropHeldItem();
                        
                        if (pawnChessPiece.GetComponent<chessValuedItem>().checkIfRecent()>0)
                        {
                            pawnChessPiece.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                            pawnChessPiece.GetComponent<chessValuedItem>().changeCurrentLocation(currentSpot);
                            if (pawnChessPiece.GetComponent<chessValuedItem>().correctLocation == currentSpot)
                            {
                                chessboardParent.GetComponent<chessBoardPlacing>().checkPuzzleCompletion();
                            }
                        }
                        if (queenChessPiece.GetComponent<chessValuedItem>().checkIfRecent() > 0)
                        {
                            queenChessPiece.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                            queenChessPiece.GetComponent<chessValuedItem>().changeCurrentLocation(currentSpot);
                            if (queenChessPiece.GetComponent<chessValuedItem>().correctLocation == currentSpot)
                            {
                                chessboardParent.GetComponent<chessBoardPlacing>().check = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public bool getIfOccupied()
    {
        return setOccupied;
    }

    public void toggleOccupied()
    {
        setOccupied = !setOccupied;
    }

    public void moveToCorrectPosition(Vector2 aBoardPosition, float yValue)
    {
        const float locationMult = 0.125f;
        const float offset = -0.4375f;
        currentSpot = aBoardPosition;
        transform.localPosition = new Vector3(offset + (aBoardPosition.x * locationMult), yValue, offset + (aBoardPosition.y * locationMult));
    }
}
