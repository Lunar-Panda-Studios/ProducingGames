using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToDoor : MonoBehaviour
{
    internal Interaction interaction;
    [Tooltip("Lobby door 1")]
    public GameObject LinkedDoor1;
    [Tooltip("Lobby door 2")]
    public GameObject LinkedDoor2;
    [Tooltip("Wife Room door 1")]
    public GameObject LinkedDoor3;
    [Tooltip("Wife Room door 2")]
    public GameObject LinkedDoor4;

    internal bool switchState = false;
    InteractRaycasting ray;

    private void Start()
    {
        interaction = GetComponent<Interaction>();
        ray = FindObjectOfType<InteractRaycasting>();
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
        if (GetComponent<Interaction>().canInteract)
        {
            if (switchState)
            {
                LinkedDoor1.GetComponent<TempSlidingDoors>().openDoor();
                LinkedDoor2.GetComponent<TempSlidingDoors>().openDoor();
                LinkedDoor3.GetComponent<TempSlidingDoors>().closeDoor();
                LinkedDoor4.GetComponent<TempSlidingDoors>().closeDoor();
            }
            else
            {
                LinkedDoor1.GetComponent<TempSlidingDoors>().closeDoor();
                LinkedDoor2.GetComponent<TempSlidingDoors>().closeDoor();
                LinkedDoor3.GetComponent<TempSlidingDoors>().openDoor();
                LinkedDoor4.GetComponent<TempSlidingDoors>().openDoor();
            }
        }

        switchState = !switchState;
    }
}
