using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    JournalMenuToggle journalToggle;
    public KeyCode turnLeft = KeyCode.E;
    public KeyCode turnRight = KeyCode.Q;
    public Inventory inventory;
    bool showingDoc = false;
    bool showText = false;
    DocumentData data;
    internal float timer = 2;
    public float delay = 1;

    private void Start()
    {
        journalToggle = FindObjectOfType<JournalMenuToggle>();
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;


        if (journalToggle.IsOnMenu && !showingDoc)
            {
                if (Input.GetKeyDown(turnLeft))
                {
                    UIManager.Instance.turnPage(true);
                timer = delay + 1;
                }

                if (Input.GetKeyDown(turnRight))
                {
                    UIManager.Instance.turnPage(false);
                timer = delay + 1;
            }
            }



            if (Input.GetButtonDown("Interact") && showingDoc)
            {
                UIManager.Instance.hideDocument(data, null);
                UIManager.Instance.hideText(null);
                showingDoc = false;
                timer = 0;
            }

            if (Input.GetButtonDown("ShowText") && showingDoc)
            {
                if (showText)
                {
                    UIManager.Instance.hideText(null);
                    showText = false;
                }
                else
                {
                    UIManager.Instance.showingText(data, null);
                    showText = true;
                }
            }
    }

    public void viewSelectedDocument(bool leftPage)
    {
        if (!showingDoc && delay < timer)
        {
            if (leftPage)
            {
                data = inventory.documentInventory[UIManager.Instance.leftPage];
            }
            else
            {
                data = inventory.documentInventory[UIManager.Instance.rightPage];
            }

            UIManager.Instance.showDocument(data, null);
            showingDoc = true;
        }
    }
}
