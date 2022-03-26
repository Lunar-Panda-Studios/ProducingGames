using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikeLock : MonoBehaviour
{
    public string audioClipName;
    [Header("Codes")]
    [Tooltip("The combination that solves the puzzle")]
    public int[] correctCode;
    [Tooltip("The combination displayed before the player has interacted with it")]
    public int[] currentCode;

    [Header("Puzzle State")]
    [Tooltip("Check for if the puzzle is solved")]
    public bool puzzleSolved;
    public GameObject door;

    bool finished;

    // Update is called once per frame
    void Update()
    {
        SetCollision();
        DisablePuzzle();
    }

    //Gives the element the current value of its place in the code
    public int getCurrentCode(int digitPlace)
    {
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
                    /*if (Analysis.current != null)
                    {
                        if (Analysis.current.consent && !Analysis.current.parameters.ContainsKey("Bike Lock"))
                        {
                            Analysis.current.resetTimer("Bike Lock");
                        }
                    }*/
                    return puzzleSolved;
                }
            }
            else
            {
                /*if (Analysis.current != null)
                {
                    Analysis.current.failCounterBikeLock++;
                }*/

                puzzleSolved = false;
                return false;
            }
        }
        return false;
    }

    void SetCollision()
    {
        if (puzzleSolved && !finished)
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX(audioClipName);
            door.GetComponent<Animator>().SetTrigger("DoorUnlocked");
            finished = true;
        }
    }

    private void DisablePuzzle()
    {
        if (puzzleSolved)
        {
            Destroy(GetComponentInChildren<bikeLockNumber>());
            Destroy(GetComponentInChildren<InteractSound>());
        }
    }
}
