using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    PlayerPickup pickup;
    public List<ItemData> destroyer;
    private List<bool> inSlot;

    public ItemData Hammer;
    public Inventory inventoryScript;

    InteractRaycasting raycast;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHitObj();
        }
    }

    void CheckHitObj()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (raycast.raycastInteract(out hit))
        {
            //if(inventoryScript.itemInventory[inventoryScript.selectedItem])
        }
    }
    //public void destroyerSelected(ItemData item)
    //{
    //    for (int i = 0; i < destroyer.Count; i++)
    //    {
    //        if (destroyer[i] == item)
    //        {
    //            if (Input.GetButtonDown("Fire1"))
    //            {
    //                Instantiate(destroyedVersion, transform.position, transform.rotation);
    //                Destroy(gameObject);
    //            }
    //        }
    //    }        
    //}
}
