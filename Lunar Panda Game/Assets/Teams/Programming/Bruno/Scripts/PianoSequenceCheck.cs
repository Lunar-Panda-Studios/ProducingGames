using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSequenceCheck : MonoBehaviour
{
    [Tooltip("id in relation to the event manager")]
    public int id;

    int SequenceLenght;
    int placeinSequence;

    public string sequence = "";
    public string attemptedSequence;    

    public void Start()
    {
        SequenceLenght = sequence.Length;
        GameEvents.current.puzzleCompleted += puzzleComplete;
    }


    void CheckCode()
    {
        if (attemptedSequence == sequence)
        {
            GameEvents.current.onPuzzleComplete(id);
            if (FindObjectOfType<Analysis>() != null)
            {
                if (Analysis.current.consent)
                {
                    Analysis.current.resetTimer("Piano");
                }
            }
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
        placeinSequence++;

        if (placeinSequence <= SequenceLenght)
        {
            attemptedSequence += value;
        }

        if (placeinSequence == SequenceLenght)
        {
            CheckCode();

            attemptedSequence = "";
            placeinSequence = 0;
        }
    }

    public void resetPuzzle(int id)
    {
        if (id == this.id)
        {
            //toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
            attemptedSequence = "";
        }
    }

    public void puzzleComplete(int id)
    {
        if (id == this.id)
        {
            //StartCoroutine(Open());
            PuzzleData.current.completedEvents[id] = true;
        }
    }
}
