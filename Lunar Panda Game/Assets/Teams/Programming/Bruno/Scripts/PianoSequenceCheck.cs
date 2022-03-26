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

    [Header("Reset Sound")]
    public string resetSound;

    [Header("Open Sound")]
    public string openSound;

    public Transform toOpen;

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
            if (Analysis.current != null)
            {
                if (Analysis.current.consent)
                {
                    Analysis.current.resetTimer("Piano");
                }
            }
        }
        else
        {
            if (Analysis.current != null)
            {
                Analysis.current.failCounterPiano++;
            }
            SoundEffectManager.GlobalSFXManager.PlaySFX(resetSound);
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

    IEnumerator Open() //Rotates the door
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(openSound);
        toOpen.Rotate(new Vector3(0, 0, -54), Space.World);

        yield return new WaitForSeconds(4); //In case we want something to happen after uncomment bellow 

        //toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
    }

    public void resetPuzzle(int id)
    {
        if (id == this.id)
        {
            toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
            attemptedSequence = "";
        }
    }

    public void puzzleComplete(int id)
    {
        if (id == this.id)
        {
            StartCoroutine(Open());
            PuzzleData.current.completedEvents[id] = true;
        }
    }
}