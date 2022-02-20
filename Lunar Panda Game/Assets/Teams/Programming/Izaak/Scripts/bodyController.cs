using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyController : MonoBehaviour
{
    public int patientID;
    public bool isCorrect;

    private bool isCut;

    public ItemData scalpelData;
    public ItemData screwdriverData;
    public GameObject screwdriver;

    private Inventory inventoryScript;
    // Start is called before the first frame update
    void Start()
    {
        isCut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (inventoryScript.itemInventory[inventoryScript.selectedItem] == scalpelData)
            {
                //Cutscene or animation or whatever will go here
                isCut = true;
                if (isCorrect == true)
                {
                    screwdriver.SetActive(true);
                }
            }
        }
    }
}
