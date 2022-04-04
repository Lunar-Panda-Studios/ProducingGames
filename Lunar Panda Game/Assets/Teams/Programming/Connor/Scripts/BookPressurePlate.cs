using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPressurePlate : MonoBehaviour
{
    [Tooltip("Enter Name of Book in inspector")]
    public string bookName;
    public ship ship;//Matej changes

    public float weightNeeded;
    private float bookWeight;
    public GameObject evilBook;


    void Start()
    {
        //weightNeeded = GetComponent<BookPuzzle>().weightNeeded;
    }

    void Update()
    {
        bookWeight = evilBook.transform.localScale.x * 10;
    }

    void OnCollisionEnter(Collision collision)
    {
        print("COllision");
        if (collision.gameObject.name == "EvilBook Variant" && bookWeight >= weightNeeded)
        {
            print("Completed");
            ship.MoveShip();
            //safeDoor.GetComponent<openSafe>().toggleOpening(true);
            //safeDoor.SetActive(false);

            if (Analysis.current != null)
            {
                if (Analysis.current.consent && (!Analysis.current.timersPuzzlesp2.ContainsKey("Perspective") && !Analysis.current.timersPuzzlesp1.ContainsKey("Perspective")))
                {
                    Analysis.current.resetTimer("Perspective");
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
 
        if (collision.gameObject.name == "EvilBook Variant" && bookWeight >= weightNeeded)
        {
            //safeDoor.GetComponent<openSafe>().toggleOpening(false);
        }
    }
}
