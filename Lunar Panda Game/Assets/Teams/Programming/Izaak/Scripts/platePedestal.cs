using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platePedestal : MonoBehaviour
{
    [HideInInspector]
    public int symbolLocation;

    [Header("Item Data")]
    [Tooltip("Item data for first item plate")]
    public ItemData plateData1;
    [Tooltip("Item data for second item plate")]
    public ItemData plateData2;

    private GameObject relativePlate;

    private bool active = true;
    private GameObject player;
    private Transform cam;

    [Header("Scripts")]
    [Tooltip("Script of the inventory system")]
    private Inventory inventoryScript;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryScript = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (active)
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                            if (inventoryScript.itemInventory[inventoryScript.selectedItem] != null)
                            {
                                if (inventoryScript.itemInventory[inventoryScript.selectedItem] == plateData1)
                                {
                                    active = false;
                                    relativePlate.SetActive(true);
                                    inventoryScript.removeItem();
                                    gameObject.SetActive(false);
                                }
                                if (inventoryScript.itemInventory[inventoryScript.selectedItem] == plateData2)
                                {
                                    active = false;
                                    relativePlate.SetActive(true);
                                inventoryScript.removeItem();
                                gameObject.SetActive(false);
                                }
                            }
                    }
                }
            }
        }
    }

    public void changePlate(GameObject plate)
    {
        relativePlate = plate;
    }
}
