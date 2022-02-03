using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //inventory system that can hold multiple items 
    public List<ItemData> inventory;
    private int selectedItem = 0;

    private int itemsIn;
    public KeyCode drop;
    public KeyCode changeItem;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<ItemData>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(drop) && itemsIn != 0)
        {
            removeItem(inventory[selectedItem], true);
        }
        if (Input.GetKeyDown(changeItem))
        {
            selectItem();
        }
    }

    public void addItem(ItemData data)
    {
        inventory.Add(data);

        itemsIn++;
    }

    public void removeItem(ItemData data, bool dropped)
    {
        //If item is used then it will not be dropped on the floor
        if (dropped)
        {
            //vector3 would be player location
            Instantiate(data.prefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        inventory.Remove(data);
        itemsIn--;

        //checks if the selected item is still in range then 
        if(selectedItem > itemsIn - 1)
        {
            selectedItem--;
        }
        if(selectedItem < 0)
        {
            selectedItem = 0;
        }
    }

    private void selectItem()
    {
        selectedItem++;

       if (selectedItem > itemsIn - 1)
        {
            selectedItem = 0;
        }
    }

}
