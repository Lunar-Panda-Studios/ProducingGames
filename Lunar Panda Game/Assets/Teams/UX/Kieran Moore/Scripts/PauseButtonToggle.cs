using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonToggle : MonoBehaviour
{

    public bool IsPaused;
    public bool IsOnRegularMenu;
    public GameObject PauseMenu;
    public lockMouse MrCapsule;
    public GameObject BarOfStamina;
    public JournalMenuToggle Journal;
    public InventoryMenuToggle Inventory;
    public FeedbackToggle Feedback;

    // Start is called before the first frame update
    void Start()
    {
        MrCapsule = FindObjectOfType<lockMouse>();
        Journal = FindObjectOfType<JournalMenuToggle>();
        Inventory = FindObjectOfType<InventoryMenuToggle>();
        Feedback = FindObjectOfType<FeedbackToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsPaused == false && Journal.IsOnMenu == false && Inventory.IsOnInventory == false && Feedback.IsOnFeedbackMenu == false)
            {
                IsPaused = true;
                BarOfStamina.SetActive(false);
                IsOnRegularMenu = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                print("Cursor is visible");
                MrCapsule.canLook = false;
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (IsPaused == true)
            {
                if (IsOnRegularMenu == true)
                {
                    PauseMenu.SetActive(false);
                    IsPaused = false;
                    BarOfStamina.SetActive(true);
                    IsOnRegularMenu = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    MrCapsule.canLook = true;
                    Cursor.visible = false;
                    print("Cursor is no longer visible");
                    Time.timeScale = 1f;
                }
            }
        }
    }
}