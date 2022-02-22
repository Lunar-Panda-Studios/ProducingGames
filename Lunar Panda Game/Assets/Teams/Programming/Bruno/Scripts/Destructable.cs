using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;      

    public ItemData Hammer;
    public Inventory inventoryScript;

    InteractRaycasting raycast;
    private void Start()
    {
        raycast = FindObjectOfType<InteractRaycasting>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            destroyObject();
        }
    }

    void destroyObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (raycast.raycastInteract(out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                if (inventoryScript.itemInventory[inventoryScript.selectedItem] == Hammer)
                {
                    Instantiate(destroyedVersion, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }            
        }
    }
    
}
