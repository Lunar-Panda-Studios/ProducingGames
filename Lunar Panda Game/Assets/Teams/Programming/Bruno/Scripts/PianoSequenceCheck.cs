using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSequenceCheck : MonoBehaviour
{
    [Tooltip("id in relation to the event manager")]
    public int id;   

    public GameObject[] pianoKeys;
    public GameObject[] attemptKeySeq;

    private int pianoIndex;

    public void Start()
    {        
        GameEvents.current.puzzleCompleted += puzzleComplete;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
            {
                CheckForSequence(hit);
            }            
        }
    }


    void CheckForSequence(RaycastHit hit)
    {
        if (hit.transform.gameObject == gameObject)
        {

        }
        else
        {
            for (int i = 0; i < pianoKeys.Length; i++)
            {
                if (hit.transform.gameObject == pianoKeys[i])
                {
                    attemptKeySeq[pianoIndex] = (pianoKeys[i]);
                    CheckCode();
                    pianoIndex++;
                    
                }                
            }
        }
    }

    void CheckCode()
    {
        if (attemptKeySeq == pianoKeys)
        {
            GameEvents.current.onPuzzleComplete(id);
            /*if (Analysis.current != null)
            {
                if (Analysis.current.consent)
                {
                    Analysis.current.resetTimer("Piano");
                }
            }*/
        }
        else if (pianoKeys[pianoIndex] != attemptKeySeq[pianoIndex])
        {
            /*if (Analysis.current != null)
            {
                Analysis.current.failCounterPiano++;
            }*/
            resetSequence();
            Debug.Log("Wrong Sequence");
        }
    }
    void resetSequence()
    {
        for (int i = 0; i < attemptKeySeq.Length; i++)
        {
            attemptKeySeq[i] = null;            
        }
        pianoIndex = 0;
    }

    void OpenAnim() 
    {
        // Reminder to set up the anim here once we have one
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
