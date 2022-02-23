using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewDocument : MonoBehaviour
{
    private bool showText = false;
    private bool showDoc = false;
    [Header("Document UI")]
    public GameObject text;
    public GameObject document;
    internal bool inInventory = false;
    [Tooltip("Enter the corrisponding data (scripable object) for this document")]
    public DocumentData data;

    [Header("Inputs")]
    public KeyCode keyInteract; //temp
    public KeyCode keyText; //temp
    private bool overDoc; //temp
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    //temp testing
    private void Update()
    {
        if(Input.GetKeyDown(keyInteract) && !showDoc)
        {
            RaycastHit hit;
            if(InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if(hit.transform.gameObject == gameObject)
                {
                    showDocument();
                }
            }
        }

        if(Input.GetKeyDown(keyText) && showDoc)
        {
            if(showText)
            {
                hideText();
            }
            else
            {
                showingText();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && showDoc)
        {
            hideDocument();
            hideText();
        }
    }

    public void showDocument()
    {
        document.GetComponent<Image>().sprite = data.documentImage;
        showDoc = true;
        document.SetActive(true);
        inventory.addItem(data);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    public void hideDocument()
    {
        showDoc = false;
        document.SetActive(false);
    }

    public void showingText()
    {
        text.GetComponent<Text>().text = data.docText;
        showText = true;
        text.gameObject.SetActive(true);
        //Show text when pressed
    }

    public void hideText()
    {
        showText = false;
        text.gameObject.SetActive(false);
        //Hide text when pressed
    }

    private void OnMouseEnter()
    {
        overDoc = true;
    }

    private void OnMouseExit()
    {
        overDoc = false;
    }
}
