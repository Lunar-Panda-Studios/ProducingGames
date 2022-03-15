using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuToggle : MonoBehaviour
{

    public bool IsOnInventory;
    [SerializeField] GameObject InventoryMenu;
    lockMouse mouseLock;
    playerJump jump;
    //public GameObject BarOfStamina;
    JournalMenuToggle Journal;
    PauseButtonToggle Pause;
    FeedbackToggle Feedback;


    // Start is called before the first frame update
    void Start()
    {
        mouseLock = FindObjectOfType<lockMouse>();
        Pause = FindObjectOfType<PauseButtonToggle>();
        Journal = FindObjectOfType<JournalMenuToggle>();
        Feedback = FindObjectOfType<FeedbackToggle>();
        jump = FindObjectOfType<playerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            //if the inventory ui isnt on screen
            if (IsOnInventory == false && Pause.IsPaused == false && Journal.IsOnMenu == false && Feedback.IsOnFeedbackMenu == false)
            {
                jump.enabled = false;
                IsOnInventory = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //BarOfStamina.SetActive(false);
                mouseLock.canLook = false;
                InventoryMenu.SetActive(true);
                Time.timeScale = 0f;
                
            }
            //if the inventory ui is already on screen
            else if (IsOnInventory == true)
            {
                jump.enabled = true;
                InventoryMenu.SetActive(false);
                IsOnInventory = false;
                //BarOfStamina.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                mouseLock.canLook = true;
                Cursor.visible = false;
                print("Cursor is no longer visible");
                Time.timeScale = 1f;
            }
        }
    }
}
