/*
Cant do too much with the document viewing system and the inventory stuff so I'm just gonna leave
that for now. I will also add functionality for a pause screen eventually too and a saving pop-up 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    [Header("UI Elements")]
    //[SerializeField] Slider staminaBar;
    [SerializeField] Image crosshair;
    [Tooltip("Put the parent empty object of the document system here")]
    [SerializeField] GameObject docViewingSystem;
    [SerializeField] GameObject inventorySystem;
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeDuration;
    public Text storyNotes;
    public Text notesText;
    Inventory inventory;
    public GameObject documentLandscape;
    public GameObject documentPortrait;
    typeWriterTest twt;
    [SerializeField] string audioClipName;
    bool isMoving;

    [Header("Inventory UI")]
    public List<Image> inventoryImages;
    public Text inventoryDescription;
    public Text inventoryName;
    public Image inventorySelect;
    public GameObject descriptionSection;
    public Image bottomRightItem;
    public Image bottomRightPanel;
    bool itemShowing = false;

    [Header("Journal UI")]
    internal Room currentTab;
    public GameObject trainSection;
    public GameObject hospitalSection;
    public GameObject hotelSection;
    public GameObject cabinSection;

    [System.Serializable]
    public class Page
    {
        public GameObject mainPage;
        public List<GameObject> documents;
    }

    public List<Page> pagesTrain;
    public List<Page> pagesHospital;
    public List<Page> pagesHotel;
    public List<Page> pagesCabin;

    int currentPage = 0;

    [Header("Objective UI")]
    public Text objectText;
    public GameObject objectivesTab;
    int objectiveNumber = 0;
    ObjectiveSystem objectiveSystem;

    [Header("Tooltip UI")]
    public GameObject TooltipSection;
    public Text tooltipText;
    public Image tooltipImage;

    [Header("AutoSave UI")]
    public GameObject autoSavingSection;

    FeedbackToggle feedbackToggle;
    InventoryMenuToggle inventoryMenuToggle;
    JournalMenuToggle journalMenuToggle;
    PauseButtonToggle pauseButtonToggle;

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
        inventory = FindObjectOfType<Inventory>();
        twt = GameObject.FindObjectOfType<typeWriterTest>();
        objectiveSystem = FindObjectOfType<ObjectiveSystem>();
        //updateObject();
        feedbackToggle = FindObjectOfType<FeedbackToggle>();
        inventoryMenuToggle = FindObjectOfType<InventoryMenuToggle>();
        journalMenuToggle = FindObjectOfType<JournalMenuToggle>();
        pauseButtonToggle = FindObjectOfType<PauseButtonToggle>();

        for (int i = 0; i < inventory.documentInventory.Count; i++)
        {
            activeImage(inventory.documentInventory[i]);
        }
    }

    private void Update()
    {
        
    }

    public void autoSavingPromptShow()
    {
        autoSavingSection.SetActive(true);
    }

    public void autoSavingPromptHide()
    {
        autoSavingSection.SetActive(false);
    }

    public void toolTipInteract(ToolTipType type)
    {
        TooltipSection.SetActive(true);
        tooltipText.text = type.text;
        tooltipImage.sprite = type.KeyboardSprite;
    }

    public void toolTipHide()
    {
        TooltipSection.SetActive(false);
    }

    public void itemEquip(ItemData data)
    {
        Color colour = bottomRightItem.color;

        if (data != null)
        {
            if (!isMoving)
            {
                bottomRightItem.color = new Color(colour.r, colour.g, colour.b, 1);
            }
            bottomRightItem.sprite = data.itemImage;
        }
        else
        {
            bottomRightItem.color = new Color(colour.r, colour.g, colour.b, 0);
            bottomRightItem.sprite = null;
        }

    }

    public void itemFade(bool isMove)
    {
        isMoving = isMove;

        Color colour = bottomRightPanel.color;
        Color colourItem = bottomRightItem.color;

        if (isMoving)
        {
            bottomRightPanel.color = new Color(colour.r, colour.g, colour.b, 0.10f);
            //StartCoroutine(fadeBottomRightPanel(0.10f));
            if(itemShowing)
            {
                bottomRightItem.color = new Color(colourItem.r, colourItem.g, colourItem.b, 0.10f);
                //StartCoroutine(fadeBottomRightItem(0.10f));
            }

        }
        else
        {
            bottomRightPanel.color = new Color(colour.r, colour.g, colour.b, 1);
            //StartCoroutine(fadeBottomRightPanel(1));
            if (itemShowing)
            {
                //StartCoroutine(fadeBottomRightItem(1));
                bottomRightItem.color = new Color(colourItem.r, colourItem.g, colourItem.b, 1);
            }
        }
    }

    IEnumerator fadeBottomRightPanel(float fadeAmount)
    {
        if (bottomRightPanel.color.a > fadeAmount)
        {
            while (bottomRightPanel.color.a > fadeAmount)
            {
                bottomRightPanel.color = new Color(bottomRightPanel.color.r, bottomRightPanel.color.g, bottomRightPanel.color.b, bottomRightPanel.color.a - Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (bottomRightPanel.color.a < fadeAmount)
            {
                bottomRightPanel.color = new Color(bottomRightPanel.color.r, bottomRightPanel.color.g, bottomRightPanel.color.b, bottomRightPanel.color.a + Time.deltaTime);
                yield return null;
            }
        }
    }

    IEnumerator fadeBottomRightItem(float fadeAmount)
    {

        if (bottomRightItem.color.a > fadeAmount)
        {
            while (bottomRightItem.color.a > fadeAmount)
            {
                bottomRightItem.color = new Color(bottomRightItem.color.r, bottomRightItem.color.g, bottomRightItem.color.b, bottomRightItem.color.a - Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (bottomRightItem.color.a < fadeAmount)
            {
                bottomRightItem.color = new Color(bottomRightItem.color.r, bottomRightItem.color.g, bottomRightItem.color.b, bottomRightItem.color.a + Time.deltaTime);
                yield return null;
            }
        }
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
        //init crosshair
        crosshair.gameObject.SetActive(crosshair.gameObject.activeSelf);
        //init doc viewing system
    }

    //this is just temporary and should be removed or changed after the vertical slice. Not my problem tho
    public IEnumerator FadePanelIn()
    {

        float elapsedTime = 0;
        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeGroup.alpha = elapsedTime / fadeDuration;
            yield return null;
        }
    }

    public void showDocument(DocumentData data, ViewDocument documentScript)
    {
        if (data.isLandscape)
        {
            documentLandscape.GetComponent<Image>().sprite = data.documentImage;
            documentLandscape.SetActive(true);
        }
        else
        {
            documentPortrait.GetComponent<Image>().sprite = data.documentImage;
            documentPortrait.SetActive(true);
        }

        if (documentScript != null)
        {
            if (!documentScript.inInventory)
            {
                inventory.addItem(data);
            }
            documentScript.showDoc = true;
        }
    }

    public void hideDocument(DocumentData data, ViewDocument documentScript)
    {
        if (documentScript != null)
        {
            documentScript.showDoc = false;
        }

        if (data.isLandscape)
        {
            documentLandscape.SetActive(false);
        }
        else
        {
            documentPortrait.SetActive(false);
        }

    }

    public void showingText(DocumentData data, ViewDocument documentScript)
    {
        notesText.text = data.docText;
        if (documentScript != null)
        {
            documentScript.showText = true;
        }
        notesText.transform.parent.gameObject.SetActive(true);
        //Show text when pressed
    }

    public void hideText(ViewDocument documentScript)
    {
        if (documentScript != null)
        {
            documentScript.showText = false;
        }
        notesText.transform.parent.gameObject.SetActive(false);
        //Hide text when pressed
    }
    public void inventoryItemAdd(ItemData data, int slot)
    {
        inventoryImages[slot].sprite = data.itemImage;
        inventoryImages[slot].color = new Color(inventoryImages[slot].color.r, inventoryImages[slot].color.g, inventoryImages[slot].color.b, 1);
    }

    public void inventoryItemSelected(ItemData data, int slot)
    {
        Color colour = inventoryImages[slot].color;
        Color colourSelect = inventorySelect.color;
        Color colourRightImage = bottomRightItem.color;

        if (data != null)
        {
            descriptionSection.SetActive(true);
            itemShowing = true;
            inventoryImages[slot].color = new Color(colour.r, colour.g, colour.b, 1);
            inventorySelect.color = new Color(colourSelect.r, colourSelect.g, colourSelect.b, 1);
            if (!isMoving)
            {
                bottomRightItem.color = new Color(colourRightImage.r, colourRightImage.g, colourRightImage.b, 1);
            }
            inventorySelect.sprite = data.itemImage;
            inventoryImages[slot].sprite = data.itemImage;
            inventoryName.text = data.itemName;
            inventoryDescription.text = data.description;
            data.timesChecked++;
        }
        else
        {
            descriptionSection.SetActive(false);
            itemShowing = false;
            inventoryImages[slot].color = new Color(colour.r, colour.g, colour.b, 0);
            inventorySelect.color = new Color(colourSelect.r, colourSelect.g, colourSelect.b, 0);
            bottomRightItem.color = new Color(colourRightImage.r, colourRightImage.g, colourRightImage.b, 0);
            inventorySelect.sprite = null;
            inventoryImages[slot].sprite = null;
            bottomRightItem.sprite = null;
            inventoryName.text = "";
            inventoryDescription.text = "";
        }
    }

    public void removeItemImage(int slot)
    {
        Color colour = inventoryImages[slot].color;
        Color colourSelect = inventorySelect.color;

        inventoryImages[slot].sprite = null;
        inventoryImages[slot].color = new Color(colour.r, colour.g, colour.b, 0);

        if(inventory.selectedItem == slot)
        {
            inventorySelect.color = new Color(colourSelect.r, colourSelect.g, colourSelect.b, 0);
            descriptionSection.SetActive(false);
            itemShowing = false;
            inventorySelect.sprite = null;
            inventoryName.text = "";
            inventoryDescription.text = "";
        }
    }

    public void changeTab(int newTab)
    {
        switch(newTab)
        {
            case 1:
                {
                    currentTab = Room.TRAIN;
                    trainSection.SetActive(true);
                    hospitalSection.SetActive(false);
                    hotelSection.SetActive(false);
                    cabinSection.SetActive(false);
                    break;
                }
            case 2:
                {
                    currentTab = Room.HOSPITAL;
                    trainSection.SetActive(false);
                    hospitalSection.SetActive(true);
                    hotelSection.SetActive(false);
                    cabinSection.SetActive(false);
                    break;
                }
            case 3:
                {
                    currentTab = Room.HOTEL;
                    trainSection.SetActive(false);
                    hospitalSection.SetActive(false);
                    hotelSection.SetActive(true);
                    cabinSection.SetActive(false);
                    break;
                }
            case 4:
                {
                    currentTab = Room.CABIN;
                    trainSection.SetActive(false);
                    hospitalSection.SetActive(false);
                    hotelSection.SetActive(false);
                    cabinSection.SetActive(true);
                    break;
                }
            default:
                {
                    break;
                }
        }

        currentPage = 0;

    }

    public void updateObject()
    {
        //objectText.text = objectiveSystem.currentObjective.description;
    }

    public void textToScreen(string dialogue)
    {
        twt.setupText();
        twt.dialogue = dialogue;
        twt.dialogueText.enabled = true;
        twt.playText = true;
    }

    public void diableSubtitles()
    {
        twt.dialogueText.enabled = false;
    }

    public void toggleMenuVariables()
    {
        feedbackToggle.canOpen = !feedbackToggle.canOpen;
        inventoryMenuToggle.canOpen = !inventoryMenuToggle.canOpen;
        journalMenuToggle.canOpen = !journalMenuToggle.canOpen;
        pauseButtonToggle.canOpen = !pauseButtonToggle.canOpen;
    }

    public void turningPage(bool right)
    {
        switch(currentTab)
        {
            case Room.TRAIN:
            {
                    if(right)
                    {
                        pagesTrain[currentPage].mainPage.SetActive(false);
                        currentPage++;
                        pagesTrain[currentPage].mainPage.SetActive(true);
                    }
                    else
                    {
                        pagesTrain[currentPage].mainPage.SetActive(false);
                        currentPage--;
                        pagesTrain[currentPage].mainPage.SetActive(true);
                    }
                break;
            }
            case Room.HOSPITAL:
                {
                    if (right)
                    {
                        pagesHospital[currentPage].mainPage.SetActive(false);
                        currentPage++;
                        pagesHospital[currentPage].mainPage.SetActive(true);
                    }
                    else
                    {
                        pagesHospital[currentPage].mainPage.SetActive(false);
                        currentPage--;
                        pagesHospital[currentPage].mainPage.SetActive(true);
                    }    
                    break;
                }
            case Room.HOTEL:
                {
                    if (right)
                    {
                        pagesHotel[currentPage].mainPage.SetActive(false);
                        currentPage++;
                        pagesHotel[currentPage].mainPage.SetActive(true);
                    }
                    else
                    {
                        pagesHotel[currentPage].mainPage.SetActive(false);
                        currentPage--;
                        pagesHotel[currentPage].mainPage.SetActive(true);
                    }
                    break;
                }
            case Room.CABIN:
                {
                    if (right)
                    {
                        pagesCabin[currentPage].mainPage.SetActive(false);
                        currentPage++;
                        pagesCabin[currentPage].mainPage.SetActive(true);
                    }
                    else
                    {
                        pagesCabin[currentPage].mainPage.SetActive(false);
                        currentPage--;
                        pagesCabin[currentPage].mainPage.SetActive(true);
                    }
                    break;
                }
        }
    }

    public void activeImage(DocumentData data)
    {
        switch (data.roomGottenIn)
        {
            case Room.TRAIN:
                {
                    for(int i = 0; i < pagesTrain.Count; i++)
                    {
                        for(int j = 0; j < pagesTrain[i].documents.Count; j++)
                        {
                            if (pagesTrain[i].documents[j].GetComponent<Image>().sprite == data.documentImage)
                            {
                                pagesTrain[i].documents[j].SetActive(true);
                                break;
                            }
                        }
                    }
                    break;
                }
            case Room.HOSPITAL:
                {
                    for (int i = 0; i < pagesHospital.Count; i++)
                    {
                        for (int j = 0; j < pagesHospital[i].documents.Count; j++)
                        {
                            if (pagesHospital[i].documents[j].GetComponent<Image>().sprite == data.documentImage)
                            {
                                pagesHospital[i].documents[j].SetActive(true);
                                break;
                            }
                        }
                    }
                    break;
                }
            case Room.HOTEL:
                {
                    for (int i = 0; i < pagesHotel.Count; i++)
                    {
                        for (int j = 0; j < pagesHotel[i].documents.Count; j++)
                        {
                            if (pagesHotel[i].documents[j].GetComponent<Image>().sprite == data.documentImage)
                            {
                                pagesHotel[i].documents[j].SetActive(true);
                                break;
                            }
                        }
                    }
                    break;
                }
            case Room.CABIN:
                {
                    for (int i = 0; i < pagesCabin.Count; i++)
                    {
                        for (int j = 0; j < pagesCabin[i].documents.Count; j++)
                        {
                            if (pagesCabin[i].documents[j].GetComponent<Image>().sprite == data.documentImage)
                            {
                                pagesCabin[i].documents[j].SetActive(true);
                                break;
                            }
                        }
                    }
                    break;
                }
        }
    }
}
