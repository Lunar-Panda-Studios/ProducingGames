using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScrewdriver : MonoBehaviour
{
    public ItemData screwDriverData;
    InteractRaycasting raycast;
    public GameObject moveLocation;
    bool moveto = false;
    public int speed = 3;
    Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        raycast = FindObjectOfType<InteractRaycasting>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Input.GetButtonDown("Interact"))
        {
            if (raycast.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (inventory.itemInventory[inventory.selectedItem] != null)
                    {
                        if (inventory.itemInventory[inventory.selectedItem] == screwDriverData)
                        {
                            GetComponent<MeshCollider>().enabled = false;
                            moveto = true;
                        }
                    }
                }
            }
        }

        if(moveto)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveLocation.transform.position, speed * Time.deltaTime);

            if(moveLocation.transform.position == transform.position)
            {
                moveto = false;

                if (Analysis.current != null)
                {
                    if (Analysis.current.consent && !Analysis.current.parameters.ContainsKey("Open Vent"))
                    {
                        Analysis.current.resetTimer("Open Vent");
                    }
                }
            }
        }
    }
}
