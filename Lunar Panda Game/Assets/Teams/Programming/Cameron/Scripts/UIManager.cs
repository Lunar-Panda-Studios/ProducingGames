/*
Cant do too much with the document viewing system and the inventory stuff so I'm just gonna leave
that for now. I will also add functionality for a pause screen eventually too and a saving pop-up 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    [Header("UI Elements")]
    [SerializeField] Slider staminaBar;
    [SerializeField] Image crosshair;
    [Tooltip("Put the parent empty object of the document system here")]
    [SerializeField] GameObject docViewingSystem;
    [SerializeField] GameObject inventorySystem;
    public Text storyNotes;

    void Awake()
    {
        //setting up singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        //initialises all the UI stuffs
        InitUI();
    }

    public void ChangeStaminaUsage(float value)
    {
        if (staminaBar.gameObject.activeSelf)
        {
            staminaBar.value = value;
        }
    }

    public void ToggleStaminaBar()
    {
        //toggles between active and inactive whenever this is called
        staminaBar.gameObject.SetActive(!staminaBar.gameObject.activeSelf);
    }

    public void ToggleCrosshair()
    {
        //toggles between active and inactive whenever this is called
        crosshair.gameObject.SetActive(!crosshair.gameObject.activeSelf);
    }

    public void storyNotesDisplay()
    {
        storyNotes.text = "";

        for (int i = 0; i < inventorySystem.GetComponent<Inventory>().storyNotesInventory.Count; i++)
        {
            storyNotes.text += inventorySystem.GetComponent<Inventory>().storyNotesInventory[i].description;
            storyNotes.text += "\n\n";

        }
    }

    void InitUI()
    {
        //init stamina
        staminaBar.maxValue = StaminaBar.instance.maxStam;
        staminaBar.value = staminaBar.maxValue;
        staminaBar.gameObject.SetActive(staminaBar.gameObject.activeSelf);
        //init crosshair
        crosshair.gameObject.SetActive(crosshair.gameObject.activeSelf);
        //init doc viewing system
    }
}
