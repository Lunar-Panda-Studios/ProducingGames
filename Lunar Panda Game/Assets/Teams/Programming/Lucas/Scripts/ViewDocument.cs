using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewDocument : MonoBehaviour
{
    internal bool showText = false;
    internal bool showDoc = false;
    [Header("Document UI")]
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
                    UIManager.Instance.showDocument(document, data, this);
                }
            }
        }

        if(Input.GetKeyDown(keyText) && showDoc)
        {
            if(showText)
            {
                UIManager.Instance.hideText(document, this);
            }
            else
            {
                UIManager.Instance.showingText(document, data, this);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && showDoc)
        {
            UIManager.Instance.hideDocument(document, this);
            UIManager.Instance.hideText(document, this);
        }
    }
}
