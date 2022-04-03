using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToDoor : MonoBehaviour
{
    Interaction interaction;
    [Tooltip("Surgery door 1")]
    public GameObject LinkedDoor1;
    [Tooltip("Surgery door 2")]
    public GameObject LinkedDoor2;
    [Tooltip("Lobby door 1")]
    public GameObject OtherLinkedDoor3;
    [Tooltip("Lobby door 2")]
    public GameObject OtherLinkedDoor4;
    public GameObject wifeRoomDoor1;
    public GameObject wifeRoomDoor2;
    [Tooltip("DoorToDoor")]
    public GameObject LinkedBox;
    internal bool switchState = true;
    InteractRaycasting ray;
    internal DoorToDoor doorToDoor;

    private void Start()
    {
        interaction = GetComponent<Interaction>();
        ray = FindObjectOfType<InteractRaycasting>();
        doorToDoor = FindObjectOfType<DoorToDoor>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if(hit.transform.gameObject == gameObject)
                {
                    interact();
                }
            }
        }
    }

    private void interact()
    {

        if(GetComponent<Interaction>().canInteract)
        {
            if (switchState)
            {
                //print("Close near door");
                LinkedDoor1.GetComponent<TempSlidingDoors>().closeDoor();
                LinkedDoor2.GetComponent<TempSlidingDoors>().closeDoor();
                OtherLinkedDoor3.GetComponent<TempSlidingDoors>().openDoor();
                OtherLinkedDoor4.GetComponent<TempSlidingDoors>().openDoor();
                LinkedBox.GetComponent<Interaction>().canInteract = true;
            }
            else
            {
                //print("Open near door");
                LinkedDoor1.GetComponent<TempSlidingDoors>().openDoor();
                LinkedDoor2.GetComponent<TempSlidingDoors>().openDoor();
                OtherLinkedDoor3.GetComponent<TempSlidingDoors>().closeDoor();
                OtherLinkedDoor4.GetComponent<TempSlidingDoors>().closeDoor();
                LinkedBox.GetComponent<Interaction>().canInteract = false;
                wifeRoomDoor1.GetComponent<TempSlidingDoors>().closeDoor();
                wifeRoomDoor2.GetComponent<TempSlidingDoors>().closeDoor();
                doorToDoor.switchState = false;

            }
        }

        switchState = !switchState;
    }
}
