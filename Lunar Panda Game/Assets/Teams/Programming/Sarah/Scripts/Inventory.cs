using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //inventory system that can hold multiple items 
    [SerializeField] 
    internal List<ItemData> itemInventory;
    [SerializeField] 
    internal List<DocumentData> documentInventory;
    [SerializeField]
    internal List<StoryData> storyNotesInventory;
    internal int selectedItem = 0;
    private int slotAmount = 0;
    private List<GameObject> slots;
    private autoCombineScript autoCombine;

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
        documentInventory = new List<DocumentData>();
        slots = new List<GameObject>();
        itemSpace = new List<GameObject>();
        autoCombine = FindObjectOfType<autoCombineScript>();

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
            removeItem();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            selectItem(true);
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectItem(false);
        }
        if(Input.GetButtonDown("OpenDocInv"))
        {
            toggleDocInventory();
        }
        if(Input.GetButtonDown("PutAway"))
        {
            toggleHeldItem();
        }
        if (Input.GetButtonDown("NextInvSlot"))
        {
            ++selectedItem;
            if(selectedItem > itemSpace.Count - 1)
            {
                selectedItem = 0;
            }
            selectedNumberItem(selectedItem);
        }
        if (Input.GetButtonDown("LastInvSlot"))
        {
            --selectedItem;
            if (selectedItem < 0)
            {
                selectedItem = itemSpace.Count - 1;
            }
            selectedNumberItem(selectedItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedNumberItem(9);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedNumberItem(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedNumberItem(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedNumberItem(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedNumberItem(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedNumberItem(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedNumberItem(5);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedNumberItem(6);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedNumberItem(7);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedNumberItem(8);
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
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pickupControl.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pickupControl.enabled = false;
            UIManager.Instance.storyNotesDisplay();
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
                if(autoCombine != null)
                    autoCombine.itemChecking(data);
                break;
            }
        }
    }

    public void addItem(DocumentData data)
    {
        documentInventory.Add(data);

        data.prefab.GetComponent<ViewDocument>().inInventory = true;
    }

    public void addItem(StoryData data)
    {
        storyNotesInventory.Add(data);
    }

    //Shows document based on the index of the selected item which is linked to the inventory list
    public void viewDoc(int index)
    {
        //int index = documentInventory.IndexOf(data);

        DocumentData document = documentInventory[index];
        document.prefab.GetComponent<ViewDocument>().showDocument();
    }

    //Finds doc in inventory then displays them
    public void hidDoc(DocumentData data)
    {
        int index = documentInventory.IndexOf(data);

        GameObject document = documentInventory[index].prefab;
        document.GetComponent<ViewDocument>().hideDocument();
    }

    public void removeItem()
    {
        //If item is used then it will not be dropped on the floor
        itemInventory[selectedItem] = null;
        //print(itemInventory[])
        itemsIn--;

        //itemIndicator.transform.position = itemSpace[selectedItem].transform.position;
    }

    public void selectedNumberItem(int number)
    {
        selectedItem = number;
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
            pickupControl.heldItem.SetActive(false);

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
            pickupControl.heldItem.SetActive(false);
            pickupControl.heldItem = null;
        }
    }

    private void takeout()
    {
        if(itemInventory[selectedItem] != null)
        {
            GameObject heldItem = Database.current.itemsInScene[itemInventory[selectedItem].id].gameObject;
            heldItem.transform.position = player.transform.position;
            //(itemInventory[selectedItem].prefab, player.transform.position, Quaternion.identity);
            pickupControl.PickupItem(heldItem.transform);
            heldItem.SetActive(true);
        }
    }
}
