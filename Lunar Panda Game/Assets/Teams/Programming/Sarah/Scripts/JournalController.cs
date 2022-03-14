using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    JournalMenuToggle journalToggle;
    public KeyCode turnLeft = KeyCode.E;
    public KeyCode turnRight = KeyCode.Q;

    private void Start()
    {
        journalToggle = FindObjectOfType<JournalMenuToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(journalToggle.IsOnMenu)
        {
            if(Input.GetKeyDown(turnLeft))
            {
                UIManager.Instance.turnPage(true);
            }

            if(Input.GetKeyDown(turnRight))
            {
                UIManager.Instance.turnPage(false);
            }
        }
    }
}
