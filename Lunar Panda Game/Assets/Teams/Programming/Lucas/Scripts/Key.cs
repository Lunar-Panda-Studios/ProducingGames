using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Check if all key parts are in inventory. Remove them and add key if they are

    Inventory inventory;
    Database database;
    ItemData itemData;
    //public GameObject key;

    private void Update()
    {
        //check if inventory Contains KeyPart1 && KeyPart2 && KeyPart3
        if(inventory.itemInventory.Contains(itemData))
        {
            //inventory.addItem(key);
        }
    }

}
