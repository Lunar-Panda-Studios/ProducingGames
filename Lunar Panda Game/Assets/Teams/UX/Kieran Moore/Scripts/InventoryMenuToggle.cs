using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuToggle : MonoBehaviour
{

    public bool IsOnInventory;
    public GameObject InventoryMenu;
    private lockMouse MrCapsule;
    public GameObject BarOfStamina;
    public JournalMenuToggle Journal;
    public PauseButtonToggle Pause;
    public FeedbackToggle Feedback;
    PlayerPickup pickup;


    // Start is called before the first frame update
    void Start()
    {
        MrCapsule = FindObjectOfType<lockMouse>();
        Pause = FindObjectOfType<PauseButtonToggle>();
        Journal = FindObjectOfType<JournalMenuToggle>();
        Feedback = FindObjectOfType<FeedbackToggle>();
        pickup = FindObjectOfType<PlayerPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsOnInventory == false && Pause.IsPaused == false && Journal.IsOnMenu == false && Feedback.IsOnFeedbackMenu == false)
            {
                IsOnInventory = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                BarOfStamina.SetActive(false);

                pickup.enabled = false;
                MrCapsule.canLook = false;
                InventoryMenu.SetActive(true);
                Time.timeScale = 0f;

                if (Analysis.current != null)
                {
                    Analysis.current.menuOpen = true;
                }
            }
            else if (IsOnInventory == true)
            {
                InventoryMenu.SetActive(false);
                IsOnInventory = false;

                BarOfStamina.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                MrCapsule.canLook = true;
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
