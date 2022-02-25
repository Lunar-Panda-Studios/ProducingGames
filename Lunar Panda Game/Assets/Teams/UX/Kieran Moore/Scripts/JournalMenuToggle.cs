using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalMenuToggle : MonoBehaviour
{

    public bool IsOnMenu;
    public GameObject JournalMenu;
    public lockMouse MrCapsule;
    public GameObject BarOfStamina;
    public PauseButtonToggle Pause;
    public InventoryMenuToggle Inventory;

    // Start is called before the first frame update
    void Start()
    {
        MrCapsule = FindObjectOfType<lockMouse>();
        Pause = FindObjectOfType<PauseButtonToggle>();
        Inventory = FindObjectOfType<InventoryMenuToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (IsOnMenu == false && Pause.IsPaused == false && Inventory.IsOnInventory == false)
            {
                IsOnMenu = true;

                BarOfStamina.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                print("Cursor is visible");
                MrCapsule.canLook = false;
                JournalMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (IsOnMenu == true)
            {
                JournalMenu.SetActive(false);
                IsOnMenu = false;
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
