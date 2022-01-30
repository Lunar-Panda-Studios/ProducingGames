/*
Quick^tm note from Cam:
All holdable items should have the "HoldableItem" script on it
The player should also have the "Player" tag on it (in the inspector)
I tested this in my own scene and it worked well. Due to the lack of information on what the designers want, i made it so that if the held item
hits an object, the player drops it. Can change this in the future if we want.
I was also thinking that we should maybe make an "Interact" script that any script can call and it would find the item infront of it (instead of
having one in a bunch of different scripts)

With the rotation, I will have to integrate it with the character looking code but it works pretty well. I could also change the keybindings if needed
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    Transform playerCameraTransform;
    [SerializeField] LayerMask rayMask;
    public GameObject heldItem;
    Vector3 mouseRotateStartPoint;
    Quaternion itemStartRotation;

    void Awake()
    {
        playerCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward) * 4f, Color.green);
        if (Input.GetButtonDown("Interact") && heldItem == null)
        {
            print("Interact Key/Button Pressed");
            RaycastHit hit;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward), out hit, 3f))
            {
                if (hit.transform.GetComponent<HoldableItem>())
                {
                    //could put the below in its own function
                    PickupItem(hit.transform);
                }
                
            }
        }
        else if(Input.GetButtonDown("Interact") && heldItem != null)
        {
            DropHeldItem();
        }
        RotateHeldItem();
    }

    void RotateHeldItem()
    {
        if (Input.GetButtonDown("Fire1") && heldItem != null)
        {
            mouseRotateStartPoint = Input.mousePosition;
            itemStartRotation = heldItem.transform.rotation;
            Cursor.lockState = CursorLockMode.None;
            playerCameraTransform.GetComponent<PlayerLook>().canLook = false;
        }
        else if (Input.GetButton("Fire1") && heldItem != null)
        {
            Vector2 distBetweenStartPoint = new Vector2((Input.mousePosition - mouseRotateStartPoint).x, (Input.mousePosition - mouseRotateStartPoint).y);
            heldItem.transform.rotation = itemStartRotation * Quaternion.Euler(new Vector3((distBetweenStartPoint.x / Screen.width) * -360, (distBetweenStartPoint.y / Screen.width) * -360, 0));
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerCameraTransform.GetComponent<PlayerLook>().canLook = true;
        }
    }

    void LateUpdate()
    {
        if (heldItem != null)
        {
            heldItem.transform.localPosition = new Vector3(0, 0, 1.5f);
        }
    }

    public void DropHeldItem()
    {
        heldItem.transform.parent = null;
        heldItem.GetComponent<Rigidbody>().useGravity = true;
        heldItem.GetComponent<Rigidbody>().freezeRotation = false;
        heldItem = null;
    }

    void PickupItem(Transform item)
    {
        item.parent = playerCameraTransform;
        item.localPosition = new Vector3(0, 0, 1.5f);
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().freezeRotation = true;
        heldItem = item.gameObject;
    }
}
