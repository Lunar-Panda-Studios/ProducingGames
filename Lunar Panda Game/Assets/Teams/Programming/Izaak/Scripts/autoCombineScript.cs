using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoCombineScript : MonoBehaviour
{
    [Header("Items Required")]
    [Tooltip("The list of items the player needs to create the object")]
    public List<ItemData> itemParts;
    [Tooltip("The object created by combining all the parts")]
    public ItemData combinedItem;

    [Header("Scripts")]
    [Tooltip("Script of the inventory system")]
    public Inventory inventoryScript;
    private List<bool> inInventory;
    // Start is called before the first frame update
    void Start()
    {
        inInventory = new List<bool>();
        inventoryScript = FindObjectOfType<Inventory>();
        for (int i = 0; i < itemParts.Count; i++)
        {
            inInventory.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void itemChecking(ItemData item)
    {
        for (int i = 0; i < itemParts.Count; i++)
        {
            if (itemParts[i] == item)
            {
                inInventory[i] = true;
            }   
        }
        combine();
    }

    bool canCombine()
    {
        for (int i = 0; i < inInventory.Count; i++)
        {
            if (!inInventory[i])
            {
                return false;
            }
        }
        return true;
    }

    void combine()
    {
        if (canCombine()==true)
        {
            for (int i = 0; i < inventoryScript.itemInventory.Count; i++)
            {
                for (int j = 0; j < itemParts.Count; j++)
                {
                    if (itemParts[j] == inventoryScript.itemInventory[i])
                    {
                        inventoryScript.itemInventory[i] = null;
                        inInventory[j] = false;
                    }
                }
            }
            inventoryScript.addItem(combinedItem);
        }
    }
}
