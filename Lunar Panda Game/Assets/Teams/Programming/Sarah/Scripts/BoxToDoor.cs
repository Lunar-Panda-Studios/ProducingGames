using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToDoor : MonoBehaviour
{
    public int id;
    public GameObject LinkedDoor1;
    public GameObject LinkedDoor2;
    public GameObject LinkedBox;
    bool switchState = false;
    InteractRaycasting ray;

    private void Start()
    {
        ray = FindObjectOfType<InteractRaycasting>();
        //LinkedDoor.GetComponent<OpenClose>().id = id;
        LinkedDoor1.GetComponent<TempSlidingDoors>().id = id;
        LinkedDoor2.GetComponent<TempSlidingDoors>().id = id;
        LinkedBox.GetComponent<PowerChanging>().id = id;
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
                GameEvents.current.onTriggerOpenDoor(id);
                GameEvents.current.onPowerTurnedOff(id);
            }
            else
            {
                GameEvents.current.onTriggerCloseDoor(id);
                GameEvents.current.onPowerTurnedOn(id);
            }
        }

        switchState = !switchState;
    }
}
