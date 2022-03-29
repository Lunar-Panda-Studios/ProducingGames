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

    [Header("Inventory UI")]
    public List<Image> inventoryImages;
    public Text inventoryDescription;
    public Text inventoryName;
    public Image inventorySelect;

    [Header("Journal UI")]
    internal Room currentTab;
    public Text leftPageTxt;
    public Text rightPageTxt;
    public Image leftPageImagePortrait;
    public Image rightPageImagePortrait;
    public Image leftPageImageLandscape;
    public Image rightPageImageLandscape;
    internal int leftPage = 0;
    internal int rightPage = 1;

    [Header("Objective UI")]
    public Text objectText;
    public GameObject objectivesTab;
    int objectiveNumber = 0;
    ObjectiveSystem objectiveSystem;

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
        updateObject();
    }

    private void Update()
    {
        
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

        documentScript.showDoc = true;
        inventory.addItem(data);
        documentScript.gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    public void hideDocument(ViewDocument documentScript)
    {
        documentScript.showDoc = false;

        if(documentScript.data.isLandscape)
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
        documentScript.showText = true;
        notesText.transform.parent.gameObject.SetActive(true);
        //Show text when pressed
    }

    public void hideText(ViewDocument documentScript)
    {
        documentScript.showText = false;
        notesText.transform.parent.gameObject.SetActive(false);
        //Hide text when pressed
    }
    public void inventoryItemAdd(ItemData data, int slot)
    {
        inventoryImages[slot].sprite = data.itemImage;
    }

    public void inventoryItemSelected(ItemData data, int slot)
    {
        if (data != null)
        {
            inventorySelect.sprite = data.itemImage;
            inventoryName.text = data.itemName;
            inventoryDescription.text = data.description;
            data.timesChecked++;
        }
        else
        {
            inventorySelect.sprite = null;
            inventoryName.text = "Item Name";
            inventoryDescription.text = "Item Description";
        }
    }

    public void turnPage(bool right)
    {
        if (inventory.documentInventory.Count != 0)
        {
            if (right)
            {
                leftPage += 2;
                if (leftPage >= inventory.documentInventory.Count)
                {
                    leftPage -= 2;
                }
            }
            else
            {
                leftPage -= 2;
                if (leftPage < 0)
                {
                    leftPage = 0;
                }
            }

            if (right)
            {
                rightPage += 2;
                if (rightPage >= inventory.documentInventory.Count)
                {
                    rightPage -= 2;
                }
            }
            else
            {
                rightPage -= 2;
                if (rightPage < 1)
                {
                    rightPage = 1;
                }
            }

            updatePages();
        }
    }

    public void changeTab(int newTab)
    {
        switch(newTab)
        {
            case 1:
                {
                    currentTab = Room.TRAIN;
                    UpdateJournal();
                    break;
                }
            case 2:
                {
                    currentTab = Room.HOSPITAL;
                    UpdateJournal();
                    break;
                }
            case 3:
                {
                    currentTab = Room.HOTEL;
                    UpdateJournal();
                    break;
                }
            case 4:
                {
                    currentTab = Room.CABIN;
                    UpdateJournal();
                    break;
                }
            default:
                {
                    break;
                }
        }

    }

    public void UpdateJournal()
    {
        for(int i = leftPage; i < inventory.documentInventory.Count; i++)
        {
            if(inventory.documentInventory[i].roomGottenIn == currentTab)
            {
                leftPage = i;
                break;
            }
        }

        rightPage = leftPage + 1;

        updatePages();
    }

    public void updatePages()
    {
        print(leftPage);
        print(rightPage);

        if (leftPage < inventory.documentInventory.Count && leftPage >= 0)
        {
            if (inventory.documentInventory[leftPage].isLandscape)
            {
                leftPageImageLandscape.sprite = inventory.documentInventory[leftPage].documentImage;
                leftPageImagePortrait.color = new Color(0, 0, 0, 0);
            }
            else
            {
                leftPageImagePortrait.sprite = inventory.documentInventory[leftPage].documentImage;
                leftPageImageLandscape.color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            leftPageImagePortrait.sprite = null;
            leftPageImageLandscape.sprite = null;
            leftPageImageLandscape.color = new Color(0, 0, 0, 0);
            leftPageImagePortrait.color = new Color(0, 0, 0, 0);
        }


        if (rightPage < inventory.documentInventory.Count && rightPage > 0)
        {

                if (inventory.documentInventory[leftPage].isLandscape)
                {
                    rightPageImageLandscape.sprite = inventory.documentInventory[rightPage].documentImage;
                    rightPageImagePortrait.color = new Color(0, 0, 0, 0);
                }
                else
                {
                    rightPageImagePortrait.sprite = inventory.documentInventory[rightPage].documentImage;
                    rightPageImageLandscape.color = new Color(0, 0, 0, 0);
                }
        }
        else
        {
            rightPageImagePortrait.sprite = null;
            rightPageImageLandscape.sprite = null;
            rightPageImageLandscape.color = new Color(0, 0, 0, 0);
            rightPageImagePortrait.color = new Color(0, 0, 0, 0);
        }
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
}
