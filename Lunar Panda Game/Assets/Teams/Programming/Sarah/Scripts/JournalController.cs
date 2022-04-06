using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    JournalMenuToggle journalToggle;
    public Inventory inventory;
    bool showingDoc = false;
    bool showText = false;
    DocumentData data;

    private void Start()
    {
        journalToggle = FindObjectOfType<JournalMenuToggle>();
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ShowText") && showingDoc)
        {
            UIManager.Instance.hideText(null);
            showText = false;
            showingDoc = false;
        }
    }

    public void viewSelectedDocument(DocumentData data)
    {
        if (!showingDoc)
        {
            UIManager.Instance.showingText(data, null);
            showingDoc = true;
            showText = true;
        }
    }
}
