using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToDoor : MonoBehaviour
{
    public int id;
    public GameObject LinkedDoor1;
    public GameObject LinkedDoor2;
    public GameObject LinkedDoor3;
    public GameObject LinkedDoor4;
    bool switchState = true;
    InteractRaycasting ray;

    private void Start()
    {
        ray = FindObjectOfType<InteractRaycasting>();
        //LinkedDoor1.GetComponent<OpenClose>().id = id;
        //LinkedDoor2.GetComponent<OpenClose>().id = id;
        LinkedDoor1.GetComponent<TempSlidingDoors>().id = id;
        LinkedDoor2.GetComponent<TempSlidingDoors>().id = id;
        LinkedDoor3.GetComponent<TempSlidingDoors>().id = id;
        LinkedDoor4.GetComponent<TempSlidingDoors>().id = id;
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
                GameEvents.current.onTriggerOpenDoor(id);
            }
            else
            {
                GameEvents.current.onTriggerCloseDoor(id);
            }
        }

        switchState = !switchState;

        if (Analysis.current.consent && !Analysis.current.parameters.ContainsKey("Electric Boxes"))
        {
            Analysis.current.resetTimer("Electric Boxes");
        }
    }
}
