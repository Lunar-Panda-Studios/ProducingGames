using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuToggle : MonoBehaviour
{

    public bool IsOnInventory;

    [SerializeField] GameObject InventoryMenu;
    lockMouse mouseLock;
    //public GameObject BarOfStamina;
    JournalMenuToggle Journal;
    PauseButtonToggle Pause;
    FeedbackToggle Feedback;
    PlayerPickup pickup;

    // Start is called before the first frame update
    void Start()
    {
        mouseLock = FindObjectOfType<lockMouse>();
        Pause = FindObjectOfType<PauseButtonToggle>();
        Journal = FindObjectOfType<JournalMenuToggle>();
        Feedback = FindObjectOfType<FeedbackToggle>();
        pickup = FindObjectOfType<PlayerPickup>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            //if the inventory ui isnt on screen
            if (IsOnInventory == false && Pause.IsPaused == false && Journal.IsOnMenu == false && Feedback.IsOnFeedbackMenu == false)
            {
                IsOnInventory = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                pickup.enabled = false;
                mouseLock.canLook = false;
                InventoryMenu.SetActive(true);
                Time.timeScale = 0f;

                if (Analysis.current != null)
                {
                    Analysis.current.menuOpen = true;
                }
            }
            //if the inventory ui is already on screen
            else if (IsOnInventory == true)
            {
                InventoryMenu.SetActive(false);
                IsOnInventory = false;
                Cursor.lockState = CursorLockMode.Locked;
                mouseLock.canLook = true;
                Cursor.visible = false;
                pickup.enabled = true;
                Time.timeScale = 1f;
                if (Analysis.current != null)
                {
                    Analysis.current.menuOpen = false;
                }
            }
        }
    }
}
