using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikeLock : MonoBehaviour
{
    [Header("Codes")]
    [Tooltip("The combination that solves the puzzle")]
    public int[] correctCode;
    [Tooltip("The combination displayed before the player has interacted with it")]
    public int[] currentCode;

    [Header("Puzzle State")]
    [Tooltip("Check for if the puzzle is solved")]
    public bool puzzleSolved;
    public GameObject door;

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        SetCollision();
    }

    //Gives the element the current value of its place in the code
    public int getCurrentCode(int digitPlace)
    {
        Debug.Log(currentCode[0]);
        return currentCode[digitPlace - 1];
    }

    //Changes the code of the specific value that was controlled
    public void changeCurrentCode(int placement, int value)
    {
        currentCode[placement - 1] = value;
    }

    //Checks if all of the elements match the code in the parent and returns true or false
    public bool checkPuzzleComplete()
    {
        int passes = 0;
        for (int i = 0; i < currentCode.Length; i++)
        {
            if (currentCode[i] == correctCode[i])
            {
                passes++;
                if (passes == currentCode.Length)
                {
                    puzzleSolved = true;
                    if (Analysis.current.consent && !Analysis.current.parameters.ContainsKey("Bike Lock"))
                    {
                        Analysis.current.resetTimer("Bike Lock");
                    }
                    return puzzleSolved;
                }
            }
            else
            {
                puzzleSolved = false;
                return false;
            }
        }
        return false;
    }
    void ChangeColor()
    {
        if (currentCode[0] == correctCode[0])
        {
            gameObject.transform.Find("BikeLockElement").GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.transform.Find("BikeLockElement").GetComponent<Renderer>().material.color = Color.red;
        }

        if (currentCode[1] == correctCode[1])
        {
            gameObject.transform.Find("BikeLockElement (1)").GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.transform.Find("BikeLockElement (1)").GetComponent<Renderer>().material.color = Color.red;
        }

        if (currentCode[2] == correctCode[2])
        {
            gameObject.transform.Find("BikeLockElement (2)").GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.transform.Find("BikeLockElement (2)").GetComponent<Renderer>().material.color = Color.red;
        }

        if (currentCode[3] == correctCode[3])
        {
            gameObject.transform.Find("BikeLockElement (3)").GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gameObject.transform.Find("BikeLockElement (3)").GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void SetCollision()
    {
        if (puzzleSolved)
        {
            door.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            door.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
