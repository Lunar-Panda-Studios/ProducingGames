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
    public string placeAudio;//Matej changes

    [HideInInspector]
    public bool puzzleComplete;
    Inventory inventory;
    private GameObject player;
    private Transform cam;
    InteractRaycasting raycast;
    // Start is called before the first frame update
    void Start()
    {
        //moveToCorrectPosition(correctSpot);
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = FindObjectOfType<Inventory>();
        raycast = FindObjectOfType<InteractRaycasting>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Interact"))
        {
            if (!setOccupied)
            {
                RaycastHit hit;
                if (raycast.raycastInteract(out hit))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        //player.GetComponent<PlayerPickup>().DropHeldItem();
                        
                        if (pawnChessPiece.GetComponent<chessValuedItem>().checkIfRecent()>0 || inventory.itemInventory[inventory.selectedItem] == pawnChessPiece.GetComponent<HoldableItem>().data)
                        {
                            inventory.puttingAway = false;
                            pawnChessPiece.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                            pawnChessPiece.GetComponent<chessValuedItem>().changeCurrentLocation(currentSpot);
                            pawnChessPiece.GetComponent<Rigidbody>().useGravity = true;
                            pawnChessPiece.SetActive(true);
                            inventory.removeItem();
                            if (pawnChessPiece.GetComponent<chessValuedItem>().correctLocation == currentSpot)
                            {
                                chessboardParent.GetComponent<chessBoardPlacing>().checkPuzzleCompletion();
                            }
                            SoundEffectManager.GlobalSFXManager.PlaySFX(placeAudio);//Matej edit

                            if (Analysis.current != null)
                            {
                                if (Analysis.current.consent)
                                {
                                    Analysis.current.failCounterChess++;
                                }
                            }
                        }
                        if (queenChessPiece.GetComponent<chessValuedItem>().checkIfRecent() > 0 || inventory.itemInventory[inventory.selectedItem] == queenChessPiece.GetComponent<HoldableItem>().data)
                        {
                            inventory.puttingAway = false;
                            queenChessPiece.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                            queenChessPiece.GetComponent<chessValuedItem>().changeCurrentLocation(currentSpot);
                            queenChessPiece.GetComponent<Rigidbody>().useGravity = true;
                            queenChessPiece.SetActive(true);
                            inventory.removeItem();
                            if (queenChessPiece.GetComponent<chessValuedItem>().correctLocation == currentSpot)
                            {
                                chessboardParent.GetComponent<chessBoardPlacing>().check = true;
                            }
                            SoundEffectManager.GlobalSFXManager.PlaySFX(placeAudio);//Matej edit

                            if (Analysis.current != null)
                            {
                                if (Analysis.current.consent)
                                {
                                    Analysis.current.failCounterChess++;
                                }
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
