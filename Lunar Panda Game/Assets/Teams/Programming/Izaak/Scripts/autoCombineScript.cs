using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoCombineScript : MonoBehaviour
{
    [System.Serializable]
    public class itemBuildPath
    {
        public List<ItemData> itemParts;
        public ItemData combinedItem;
    }
    [Header("Items Required")]
    [Tooltip("The list of all items that can be made through combining and their ingredients")]
    public List<itemBuildPath> autoCombineItemsList;
    
    
    //public List<ItemData> itemParts;
    //[Tooltip("The object created by combining all the parts")]
    //public ItemData combinedItem;

    [Header("Scripts")]
    [Tooltip("Script of the inventory system")]
    public Inventory inventoryScript;
    private List<List<bool>> inInventory;
    // Start is called before the first frame update
    void Start()
    {
        inInventory = new List<List<bool>>();
        inventoryScript = FindObjectOfType<Inventory>();
        for (int j = 0; j < autoCombineItemsList.Count; j++)
        {
            for (int i = 0; i < autoCombineItemsList[j].itemParts.Count; i++)
            {
                inInventory[j].Add(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void itemChecking(ItemData item)
    {
        for (int j = 0; j < autoCombineItemsList.Count; j++)
        {
            for (int i = 0; i < autoCombineItemsList[j].itemParts.Count; i++)
            {
                if (autoCombineItemsList[j].itemParts[i] == item)
                {
                    inInventory[j][i] = true;
                }
            }
        }
        combine();
    }

    bool canCombine()
    {
        for (int j = 0; j < autoCombineItemsList.Count; j++)
        {
            for (int i = 0; i < inInventory[j].Count; i++)
            {
                if (!inInventory[j][i])
                {
                    return false;
                }
            }
        }
        return true;
    }

    void combine()
    {
        if (canCombine()==true)
        {
            for (int k = 0; k < autoCombineItemsList.Count; k++)
            {
                for (int i = 0; i < inventoryScript.itemInventory.Count; i++)
                {

                    for (int j = 0; j < autoCombineItemsList[k].itemParts.Count; j++)
                    {
                        if (autoCombineItemsList[k].itemParts[j] == inventoryScript.itemInventory[i])
                        {
                            inventoryScript.itemInventory[i] = null;
                            inInventory[k][j] = false;
                        }
                    }

                }
                inventoryScript.addItem(autoCombineItemsList[k].combinedItem);
            }
            
        }
    }
}
