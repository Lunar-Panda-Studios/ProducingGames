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

    [Header("Puzzle Selection")]
    [Tooltip("Which element of the lock that is currently being used")]
    public int currentSelectedElement;

    [Header("Puzzle State")]
    [Tooltip("Check for if the puzzle is solved")]
    public bool puzzleSolved;

    // Update is called once per frame
    void Update()
    {
        //Placeholder inputs to change the selected element
        if (Input.GetKeyDown("left"))
        {
            if (currentSelectedElement > 1)
            {
                currentSelectedElement--;
            }
        }
        if (Input.GetKeyDown("right"))
        {
            if (currentSelectedElement < currentCode.Length)
            {
                currentSelectedElement++;
            }
        }
    }

    //Function that checks if the element controlled is available to move
    public int getSelectedElement()
    {
        return currentSelectedElement;
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
}
