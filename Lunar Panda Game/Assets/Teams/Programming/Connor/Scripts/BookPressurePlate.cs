using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPressurePlate : MonoBehaviour
{
    [Tooltip("Enter Name of Book in inspector")]
    public string bookName;

    private float weightNeeded;
    private float bookWeight;
    public GameObject evilBook;
    [Tooltip("Drag the safe's door here")]
    public GameObject safeDoor;

    void Start()
    {
        //weightNeeded = GetComponent<BookPuzzle>().weightNeeded;
    }

    void Update()
    {
        bookWeight = evilBook.transform.localScale.x * 10;
    }

    private void OnTriggerEnter(Collider pog)
    {
        if (pog.gameObject.name == "EvilBook" && bookWeight > weightNeeded)
        {
            safeDoor.GetComponent<openSafe>().toggleOpening(true);
        }
    }

    private void OnTriggerExit(Collider pog)
    {
 
        if (pog.gameObject.name == "EvilBook" && bookWeight > weightNeeded)
        {
            safeDoor.GetComponent<openSafe>().toggleOpening(false);
        }
    }
}
