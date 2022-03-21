using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public ItemData key;
    Inventory inventory;
    bool moving = false;
    public int moveTotal;
    int currentTotal;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if(key == inventory.itemInventory[inventory.selectedItem])
                    {
                        open();
                    }
                }
            }
        }

        if(moving)
        {
            currentTotal += 1;
            
            transform.parent.transform.Rotate(new Vector3(0, 1, 0));

            if(moveTotal == currentTotal)
            {
                moving = false;
            }
        }
    }

    void open()
    {
        moving = true;
    }
}
