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
    [Tooltip("Key to put away or take out item")]
    public KeyCode putAwayKey;

    [Header("Inventory UI")]
    [Tooltip("This would the gameobject that has the UI for the document inventory in")]
    public GameObject docInventory;
    public GameObject itemInventoryUI;
    lockMouse mouse;
    playerMovement player;
    public GameObject itemIndicator;
    List<GameObject> itemSpace;

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
        itemSpace = new List<GameObject>();

        foreach (Transform child in docInventory.transform)
        {
            slots.Add(child.gameObject);
        }

        foreach(Transform child in itemInventoryUI.transform)
        {
            if(child.tag != "Ignore")
            {
                itemSpace.Add(child.gameObject);
                itemInventory.Add(null);
            }
        }
    }

    //Likely a temp untill placed elsewhere
    private void Update()
    {
        if (Input.GetKeyDown(drop) && itemsIn != 0)
        {
            removeItem(itemInventory[selectedItem]);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            selectItem(true);
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectItem(false);
        }
        if(Input.GetKeyDown(openDocKey))
        {
            toggleDocInventory();
        }
        if(Input.GetKeyDown(putAwayKey))
        {
            toggleHeldItem();
        }
    }

    public void toggleHeldItem()
    {
        if(pickupControl.heldItem == null)
        {
            takeout();
        }
        else
        {
            putAway();
        }
    }

    //Toggles the inventory from open/close and changing settings so that mouse can be used
    public void toggleDocInventory()
    {
        player.enabled = !player.enabled;
        mouse.canLook = !mouse.canLook;
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
        for(int i = 0; i < itemInventory.Count; i++)
        {
            if(itemInventory[i] == null)
            {
                itemInventory[i] = data;
                itemsIn++;
                break;
            }
        }
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

    public void removeItem(ItemData data)
    {
        //If item is used then it will not be dropped on the floor
        itemInventory.Remove(data);
        itemsIn--;

        itemIndicator.transform.position = itemSpace[selectedItem].transform.position;
    }

    private void selectItem(bool positive)
    {
        if(positive)
        {
            selectedItem++;
        }
        else
        {
            selectedItem--;
        }


       if (selectedItem > itemSpace.Count - 1)
        {
            selectedItem = 0;
        }

       if(selectedItem < 0)
        {
            selectedItem = itemSpace.Count - 1;
        }

       if(pickupControl.heldItem != null)
        {
            Destroy(pickupControl.heldItem);

            if(itemInventory[selectedItem] != null)
            {
                takeout();
            }
        }

        itemIndicator.transform.position = itemSpace[selectedItem].transform.position;
    }

    private void putAway()
    {
        if(pickupControl.heldItem != null)
        {
            Destroy(pickupControl.heldItem);
            pickupControl.heldItem = null;
        }
    }

    private void takeout()
    {
        if(itemInventory[selectedItem] != null)
        {
            GameObject heldItem = Instantiate(itemInventory[selectedItem].prefab, player.transform.position, Quaternion.identity);
            pickupControl.PickupItem(heldItem.transform);
        }
    }


}
