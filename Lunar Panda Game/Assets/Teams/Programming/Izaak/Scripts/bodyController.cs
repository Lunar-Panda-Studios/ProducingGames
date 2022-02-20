using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyController : MonoBehaviour
{
    [Header("Puzzle Values")]
    [Tooltip("The ID of this patient")]
    public int patientID;
    [Tooltip("Whether this patient is the one with the screwdriver or not")]
    public bool isCorrect;

    private bool isCut;

    [Header("Item Data")]
    [Tooltip("Item data for the scalpel")]
    public ItemData scalpelData;
    [Tooltip("Screwdriver game object (only needed for the body with the screwdriver)")]
    public GameObject screwdriver;
    private bool collected = false;
    private GameObject player;
    private Transform cam;

    private Inventory inventoryScript;

    void Awake()
    {
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = FindObjectOfType<Inventory>();
        isCut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (inventoryScript.itemInventory[inventoryScript.selectedItem] != null)
                    {
                        if (inventoryScript.itemInventory[inventoryScript.selectedItem] == Database.current.itemsInScene[inventoryScript.itemInventory[inventoryScript.selectedItem].id].data)
                        {
                            //Cutscene or animation or whatever will go here
                            isCut = true;
                            if (isCorrect == true)
                            {
                                if (collected == false)
                                {
                                    screwdriver.SetActive(true);
                                    collected = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
