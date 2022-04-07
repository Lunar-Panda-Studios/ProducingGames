using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    //inventory system that can hold multiple items 
    [SerializeField] 
    //internal List<ItemData> itemInventory;
    public List<ItemData> itemInventory;
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
    internal bool puttingAway = false;
    GameObject puttingAwayItem;
    float lerpSpeed = 5;
    Camera cam;
    float timer = 0;
    public float maxTime = 1;





    private void Awake()
    {
        if (GameManager.Instance.docInventory.Count != 0)
        {
            documentInventory = GameManager.Instance.docInventory;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>().gameObject;
        pickupControl = FindObjectOfType<PlayerPickup>();
        itemInventory = new List<ItemData>();
        documentInventory = new List<DocumentData>();
        autoCombine = FindObjectOfType<autoCombineScript>();
        cam = FindObjectOfType<Camera>();

        for(int i = 0; i < maxItemsInInventory; i++)
        {
            itemInventory.Add(null);
        }

    }

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f && !pickupControl.holdingNarrative)
        {
            selectItem(true);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f && !pickupControl.holdingNarrative)
        {
            selectItem(false);
        }
        if (Input.GetButtonDown("PutAway") && !pickupControl.holdingNarrative)
        {
            toggleHeldItem();
        }
        if(Input.GetButtonDown("Interact") && itemInventory[selectedItem] != null)
        {
            if (pickupControl.heldItem != null)
            {
                if (!pickupControl.holdingNarrative)
                {
                    itemInventory[selectedItem].timesUses++;
                }
            }

        }

        if (puttingAway)
        {
            timer += Time.deltaTime;
            puttingAwayItem.transform.position = Vector3.MoveTowards(puttingAwayItem.transform.position, player.transform.position, lerpSpeed * Time.deltaTime);

            if (puttingAwayItem.transform.position == cam.transform.position || timer >= maxTime)
            {
                timer = 0;
                puttingAway = false;
                puttingAwayItem.SetActive(false);
                puttingAwayItem = null;
            }
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
        bool notInInventory = true; 
        for (int i = 0; i < itemInventory.Count; i++)
        {
            if(itemInventory[i] == data)
            {
                notInInventory = false;
            }
        }

        if (notInInventory)
        {
            for (int i = 0; i < itemInventory.Count; i++)
            {
                if (itemInventory[i] == null)
                {
                    itemInventory[i] = data;
                    UIManager.Instance.inventoryItemAdd(data, i);
                    itemsIn++;
                    break;
                }
            }
            data.beenPickedUp = true;
            UIManager.Instance.inventoryItemSelected(itemInventory[selectedItem], selectedItem);
            UIManager.Instance.itemEquip(itemInventory[selectedItem]);
        }
    }

    public void addItem(DocumentData data)
    {
        documentInventory.Add(data);
        GameManager.Instance.docInventory.Add(data);
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

        UIManager.Instance.inventoryItemSelected(itemInventory[selectedItem], selectedItem);
        UIManager.Instance.itemEquip(itemInventory[selectedItem]);
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
        UIManager.Instance.itemEquip(itemInventory[selectedItem]);
    }

    public void selectItem(int inventoryNumber)
    {
        selectedItem = inventoryNumber;
        UIManager.Instance.inventoryItemSelected(itemInventory[selectedItem], inventoryNumber);
        UIManager.Instance.itemEquip(itemInventory[selectedItem]);
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


            puttingAway = true;
            puttingAwayItem = pickupControl.heldItem;
            pickupControl.heldItem = null;
        }
    }

    private void takeout()
    {
        if(itemInventory[selectedItem] != null)
        {
            puttingAway = false;
            GameObject heldItem = Database.current.itemsInScene[itemInventory[selectedItem].id].gameObject;
            heldItem.transform.position = player.transform.position;
            pickupControl.PickupItem(heldItem.transform);
            heldItem.SetActive(true);
        }
    }
}
