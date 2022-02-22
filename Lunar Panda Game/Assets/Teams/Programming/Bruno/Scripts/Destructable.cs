using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    PlayerPickup pickup;
    public List<ItemData> destroyer;
    private List<bool> inSlot;

    public Inventory inventoryScript;


    private void Update()
    {
        
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
