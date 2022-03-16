using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private autoCombineScript autoCombine;
    public int maxItemsInInventory = 12;

    private int itemsIn;
    PlayerPickup pickupControl;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>().gameObject;
        pickupControl = FindObjectOfType<PlayerPickup>();
        itemInventory = new List<ItemData>();
        documentInventory = new List<DocumentData>();
        autoCombine = FindObjectOfType<autoCombineScript>();

        for(int i = 0; i < maxItemsInInventory; i++)
        {
            itemInventory.Add(null);
        }

    }

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectItem(true);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            selectItem(false);
        }
        if (Input.GetButtonDown("PutAway"))
        {
            toggleHeldItem();
        }
        if(Input.GetButtonDown("Interact") && itemInventory[selectedItem] != null)
        {
            itemInventory[selectedItem].timesUses++;
        }
    }

    public void toggleHeldItem()
    {
        if (pickupControl.heldItem == null)
        {
            takeout();
        }
        else
        {
            for (int i = 0; i < maxItemsInInventory; i++)
            {
                if(itemInventory[i] != null)
                {
                    if (itemInventory[i].id == pickupControl.heldItem.GetComponent<HoldableItem>().data.id)
                    {
                        putAway();
                        break;
                    }
                }
            }
        }
    }

    public void addItem(ItemData data)
    {
        for(int i = 0; i < itemInventory.Count; i++)
        {
            if(itemInventory[i] == null)
            {
                itemInventory[i] = data;
                UIManager.Instance.inventoryItemAdd(data, i);
                itemsIn++;
                break;
            }
        }
        data.beenPickedUp = true;
    }

    public void addItem(DocumentData data)
    {
        documentInventory.Add(data);

        data.prefab.GetComponent<ViewDocument>().inInventory = true;
        data.beenPickedUp = true;
    }

    public void addItem(StoryData data)
    {
        storyNotesInventory.Add(data);
        data.beenPickedUp = true;
    }

    public void removeItem()
    {
        //If item is used then it will not be dropped on the floor
        itemInventory[selectedItem] = null;
        itemsIn--;
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


       if (selectedItem > maxItemsInInventory - 1)
        {
            selectedItem = 0;
        }

       if(selectedItem < 0)
        {
            selectedItem = maxItemsInInventory - 1;
        }

       if(pickupControl.heldItem != null)
        {
            pickupControl.heldItem.SetActive(false);

            if(itemInventory[selectedItem] != null)
            {
                takeout();
            }
        }

        UIManager.Instance.inventoryItemSelected(itemInventory[selectedItem], selectedItem);
    }

    public void selectItem(int inventoryNumber)
    {
        selectedItem = inventoryNumber;
        UIManager.Instance.inventoryItemSelected(itemInventory[selectedItem], inventoryNumber);
    }

    private void putAway()
    {
        if(pickupControl.heldItem != null)
        {
            if (pickupControl.heldItem != null)
            {
                if (autoCombine != null)
                {
                    autoCombine.itemChecking(pickupControl.heldItem.GetComponent<HoldableItem>().data);
                }
            }
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
            pickupControl.PickupItem(heldItem.transform);
            heldItem.SetActive(true);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level < 3)
        {
            pickupControl = FindObjectOfType<PlayerPickup>();
            autoCombine = FindObjectOfType<autoCombineScript>();
            itemInventory.Clear();
        }
        else
        {
            Destroy(this);
        }
    }
}
