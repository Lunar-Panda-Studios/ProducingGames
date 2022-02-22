using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToDoor : MonoBehaviour
{
    public int id;
    public GameObject LinkedDoor;
    public GameObject LinkedBox;
    bool switchState = true;
    InteractRaycasting ray;

    private void Start()
    {
        ray = FindObjectOfType<InteractRaycasting>();
        LinkedDoor.GetComponent<OpenClose>().id = id;
        LinkedBox.GetComponent<PowerChanging>().id = id;
    }

    private void Update()
    {
        RaycastHit hit;

        if (ray.raycastInteract(out hit))
        {
                if(Input.GetButtonDown("Interact"))
                {
                    interact();
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
