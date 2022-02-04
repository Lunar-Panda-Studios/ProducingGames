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
    public KeyCode drop;
    public KeyCode changeItem;
    public KeyCode openDocKey;
    public GameObject docInventory;
    lockMouse mouse;
    playerMovement player;
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

    public void toggleDocInventory()
    {
        player.enabled = !player.enabled;
        mouse.canLook = !mouse.canLook;
        print(docInventory.activeInHierarchy);
        docInventory.SetActive(!docInventory.activeInHierarchy);

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
