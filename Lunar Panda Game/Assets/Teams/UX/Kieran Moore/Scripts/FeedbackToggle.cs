using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackToggle : MonoBehaviour
{

    public bool IsOnFeedbackMenu;
    public GameObject PauseMenu;
    public lockMouse MrCapsule;
    public GameObject BarOfStamina;
    public JournalMenuToggle Journal;
    public InventoryMenuToggle Inventory;

    // Start is called before the first frame update
    void Start()
    {
        MrCapsule = FindObjectOfType<lockMouse>();
        Journal = FindObjectOfType<JournalMenuToggle>();
        Inventory = FindObjectOfType<InventoryMenuToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsOnFeedbackMenu == false && Journal.IsOnMenu == false && Inventory.IsOnInventory == false)
            {
                IsOnFeedbackMenu = true;
                BarOfStamina.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                print("Cursor is visible");
                MrCapsule.canLook = false;
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (IsOnFeedbackMenu == true)
            {
                PauseMenu.SetActive(false);
                IsOnFeedbackMenu = false;
                BarOfStamina.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                MrCapsule.canLook = true;
                Cursor.visible = false;
                print("Cursor is no longer visible");
                Time.timeScale = 1f;
            }
        }
    }
}
