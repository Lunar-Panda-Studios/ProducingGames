using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToLights : MonoBehaviour
{
    public int id;
    bool switchState = false;
    [Tooltip("BoxToDoor")]
    public GameObject LinkedBox;
    [Tooltip("Surgery door 1")]
    public GameObject linkedDoor1;
    [Tooltip("Surgery door 2")]
    public GameObject linkedDoor2;
    [Tooltip("Lobby door 1")]
    public GameObject linkedDoor3;
    [Tooltip("Lobby door 2")]
    public GameObject linkedDoor4;
    internal DoorToDoor doorToDoor;
    internal BoxToDoor boxToDoor;

    public List<GameObject> LinkedLights;
    InteractRaycasting ray;

    private void Start()
    {
        ray = FindObjectOfType<InteractRaycasting>();
        doorToDoor = FindObjectOfType<DoorToDoor>();
        boxToDoor = FindObjectOfType<BoxToDoor>();

        for(int i = 0; i < LinkedLights.Count; i++)
        {
            foreach (Transform child in LinkedLights[i].transform)
            {
                if (!child.gameObject.CompareTag("Ignore"))
                {
                    child.GetComponent<LightsChaging>().id = id;
                }
            }
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
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
                //print("On lights");
                GameEvents.current.onTriggerLightsOn(id);
                LinkedBox.GetComponent<Interaction>().canInteract = false;
                linkedDoor1.GetComponent<TempSlidingDoors>().closeDoor();
                linkedDoor2.GetComponent<TempSlidingDoors>().closeDoor();
                linkedDoor3.GetComponent<TempSlidingDoors>().closeDoor();
                linkedDoor4.GetComponent<TempSlidingDoors>().closeDoor();
                doorToDoor.switchState = false;
                doorToDoor.interaction.canInteract = false;
                boxToDoor.switchState = true;
            }
            else
            {
                //print("Off lights");
                GameEvents.current.onTriggerLightsOff(id);
                LinkedBox.GetComponent<Interaction>().canInteract = true;
                linkedDoor1.GetComponent<TempSlidingDoors>().openDoor();
                linkedDoor2.GetComponent<TempSlidingDoors>().openDoor();
            }

            switchState = !switchState;
        }
    }
}
