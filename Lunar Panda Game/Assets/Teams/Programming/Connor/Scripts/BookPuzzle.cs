using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BookPuzzle : MonoBehaviour
{
    //Make sure the game object starts at 2 on scale as 1 is the min number and 3 is the highest

    [Tooltip("drag book here")]
    public GameObject evilBook;
    [Tooltip("drag player cam here")]
    public GameObject cameraRotate;
    [Tooltip("drag pressure plate here")]
    public GameObject pressurePlate;
    [Tooltip("weight required to unlock pressure plate corresponds to ")]
    public float weightNeeded;
    [Tooltip("maximum size of book before looking up does nothing")]
    public float maxSize = 3;
    [Tooltip("minimum size of book before looking down does nothing, additonally set the books base scale to 2")]
    public float minSize = 1;

    public float bookWeight;

    Vector3 cubeVector = new Vector3(0.002f, 0.002f, 0.002f);

    private float rotation;
    private PlayerPickup pickUp;

    private void Start()
    {
        pickUp = GameObject.FindObjectOfType<PlayerPickup>();
        pressurePlate.SetActive(false);
    }

    void Update()
    {
        //Debug.Log(cameraRotate.transform.localRotation.eulerAngles.x);

        rotation = cameraRotate.transform.localRotation.eulerAngles.x;

        if (pickUp.heldItem == gameObject)
        {
            pressurePlate.SetActive(true);
        }

        sizeIncrease();

    }

    void sizeIncrease()
    {
        //looking up
        if (rotation < 360 && rotation >= 285)
        {
            if (pickUp.heldItem == gameObject)
            {
                if (evilBook.transform.localScale.x < maxSize)
                {
                    evilBook.transform.localScale += cubeVector;
                }
            }
        }

        //looking down
        if (rotation > 0 && rotation < 90)
        {
            if (pickUp.heldItem == gameObject)
            {
                if (evilBook.transform.localScale.x > minSize)
                {
                    evilBook.transform.localScale -= cubeVector;
                }
            }
        }
    }
}

