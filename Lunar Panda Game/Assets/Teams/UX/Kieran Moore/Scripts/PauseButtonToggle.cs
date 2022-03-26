using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonToggle : MonoBehaviour
{
    public bool IsPaused;
    public bool IsOnRegularMenu;
    public GameObject PauseMenu;
    lockMouse MrCapsule;
    //public GameObject BarOfStamina;
    JournalMenuToggle Journal;
    InventoryMenuToggle Inventory;
    FeedbackToggle Feedback;
    public GameObject PauseMenuElement;

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
        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused == false && Journal.IsOnMenu == false && Inventory.IsOnInventory == false && Feedback.IsOnFeedbackMenu == false)
            {
                IsPaused = true;
                //BarOfStamina.SetActive(false);
                IsOnRegularMenu = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                MrCapsule.canLook = false;
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                
            }
            else if (IsPaused == true)
            {
               if (IsOnRegularMenu == true)
                {
                    Unpause();
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }


        if (PauseMenuElement.activeInHierarchy == false)
        {
            IsOnRegularMenu = false;
        }


        if (PauseMenuElement.activeInHierarchy == true)
        {
            IsOnRegularMenu = true;
        }
    }

    public void Unpause()
    {
       if (IsOnRegularMenu == true)
        {
            PauseMenu.SetActive(false);
            IsPaused = false;
            //BarOfStamina.SetActive(true);
            IsOnRegularMenu = false;
            Cursor.lockState = CursorLockMode.Locked;
            MrCapsule.canLook = true;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}
