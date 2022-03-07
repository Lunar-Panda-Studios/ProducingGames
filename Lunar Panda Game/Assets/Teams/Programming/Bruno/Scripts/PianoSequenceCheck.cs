using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSequenceCheck : MonoBehaviour
{
    [Tooltip("id in relation to the event manager")]
    public int id;

    int SequenceLenght;
    int placeinCode;

    public string sequence = "";
    public string attemptedCode;    

    public void Start()
    {
        SequenceLenght = sequence.Length;
        GameEvents.current.puzzleCompleted += puzzleComplete;
    }


    void CheckCode()
    {
        if (attemptedCode == sequence)
        {
            GameEvents.current.onPuzzleComplete(id);
        }
        else
        {
            Debug.Log("Wrong Code");
        }
    }

    void OpenAnim() 
    {
        // Reminder to set up the anim here once we have one
    }

    public void SetValue(string value)
    {
        placeinCode++;

        if (placeinCode <= SequenceLenght)
        {
            attemptedCode += value;
        }

        if (placeinCode == SequenceLenght)
        {
            CheckCode();

            attemptedCode = "";
            placeinCode = 0;
        }
    }

    //public void resetPuzzle(int id)
    //{
    //    if (id == this.id)
    //    {
    //        toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
    //        attemptedCode = "";
    //    }
    //}

    //public void puzzleComplete(int id)
    //{
    //    if (id == this.id)
    //    {
    //        StartCoroutine(Open());
    //        PuzzleData.current.completedEvents[id] = true;
    //    }
    //}
}
