using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //inventory system that can hold multiple items 
    internal List<ItemData> itemInventory;
    internal List<GameObject> documentInventory;
    private int selectedItem = 0;
    private int slotAmount = 0;
    private List<GameObject> slots;

    private int itemsIn;

    [Header ("Inputs")]
    [Tooltip("Key to drop item")]
    public KeyCode drop;
    [Tooltip("Key to change select inventory item")]
    public KeyCode changeItem;
    [Tooltip("Key to open the document inventory")]
    public KeyCode openDocKey;

    [Header("Inventory UI")]
    [Tooltip("This would the gameobject that has the UI for the document inventory in")]
    public GameObject docInventory;
    lockMouse mouse;
    playerMovement player;

    [Header("Item Dropping")]
    [Tooltip("Location that the item is spawned when dropped from your inventory")]
    public GameObject itemLocation;
    PlayerPickup pickupControl;

    // Start is called before the first frame update
    void Start()
    {
        //slots = GameObject.FindGameObjectsWithTag("DocSlots");
        pickupControl = FindObjectOfType<PlayerPickup>();
        mouse = FindObjectOfType<lockMouse>();
        player = FindObjectOfType<playerMovement>();
        itemInventory = new List<ItemData>();
        documentInventory = new List<GameObject>();
        slots = new List<GameObject>();

        foreach (Transform child in docInventory.transform)
        {
            slots.Add(child.gameObject);
        }
    }

    //Likely a temp untill placed elsewhere
    private void Update()
    {
        if (Input.GetKeyDown(drop) && itemsIn != 0)
        {
            removeItem(itemInventory[selectedItem], true);
        }
        if (Input.GetKeyDown(changeItem))
        {
            selectItem();
        }
        if(Input.GetKeyDown(openDocKey))
        {
            toggleDocInventory();
        }
    }

    //Toggles the inventory from open/close and changing settings so that mouse can be used
    public void toggleDocInventory()
    {
        player.enabled = !player.enabled;
        mouse.canLook = !mouse.canLook;
        print(docInventory.activeInHierarchy);
        docInventory.SetActive(!docInventory.activeInHierarchy);

        //Can't toggle thus needed to be an if statement to check if opening or closing inventory
        if (player.enabled)
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            pickupControl.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            pickupControl.enabled = false;
        }
    }

    //Changes settings to regular view controls
    public void closeDocInventory()
    {
        player.enabled = true;
        mouse.canLook = true;
        docInventory.SetActive(false);
    }

    public void addItem(ItemData data)
    {
        itemInventory.Add(data);

        itemsIn++;
    }

    public void addItem(GameObject data)
    {
        documentInventory.Add(data);

        data.GetComponent<ViewDocument>().inInventory = true;

        addSlot(data);
    }

    //Shows document based on the index of the selected item which is linked to the inventory list
    public void viewDoc(int index)
    {
        //int index = documentInventory.IndexOf(data);

        GameObject document = documentInventory[index];
        document.GetComponent<ViewDocument>().showDocument();
    }

    void addSlot(GameObject data)
    {
        //slots[slotAmount].GetComponent<Image>().sourceImage = data.GetComponent<DocumentData>().inventoryIcon;
        print(slotAmount);
        slots[slotAmount].SetActive(true);
        slotAmount++;
    }

    //Finds doc in inventory then displays them
    public void hidDoc(GameObject data)
    {
        int index = documentInventory.IndexOf(data);

        GameObject document = documentInventory[index];
        document.GetComponent<ViewDocument>().hideDocument();
    }

    public void removeItem(ItemData data, bool dropped)
    {
        //If item is used then it will not be dropped on the floor
        if (dropped)
        {
            //Drops item just infront of the player
            Instantiate(data.prefab, itemLocation.transform.position , Quaternion.identity);
        }
        itemInventory.Remove(data);
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
