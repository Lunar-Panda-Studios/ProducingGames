using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeLock : MonoBehaviour
{
    [Tooltip("id in relation to the event manager")]
    public int id;

    int codeLenght;
    int placeinCode;

    public string code = "";
    public string attemptedCode;

    //public GameObject toOpen;
    public string AudioClipName;

    public GameObject anim;


    public TextMeshPro CodeScreen;

    public void Start()
    {
        codeLenght = code.Length;
        GameEvents.current.puzzleCompleted += puzzleComplete;
    }

    public void Update()
    {
        CodeScreen.text = attemptedCode;
    }

    void CheckCode()
    {
        if(attemptedCode == code)
        {
            GameEvents.current.onPuzzleComplete(id);
            if (Analysis.current != null)
            {
                if (Analysis.current.consent)
                {
                    Analysis.current.resetTimer("Code Lock");
                }
            }
        }
        else
        {
            if (Analysis.current != null)
            {
                Analysis.current.failCounterCodeLock++;
            }
            Debug.Log("Wrong Code");
        }
    }

    IEnumerator Open() //Rotates the door
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(AudioClipName);
        //toOpen.Rotate(new Vector3(0, 90, 0), Space.World);        
        anim.GetComponent<Animator>().SetTrigger("Open");

        yield return new WaitForSeconds(4); //In case we want something to happen after uncomment bellow 

        //toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
    }

    public void SetValue(string value)
    {
        placeinCode++;

        if(placeinCode <= codeLenght)
        {
            attemptedCode += value;
        }

        if(placeinCode == codeLenght)
        {
            CheckCode();

            attemptedCode = "";
            placeinCode = 0;
        }
    }

    public void resetPuzzle(int id)
    {
        if(id == this.id)
        {
            //toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
            attemptedCode = "";
        }
    }

    public void puzzleComplete(int id)
    {
        if(id == this.id)
        {
            StartCoroutine(Open());
            PuzzleData.current.completedEvents[id] = true;
        }
    }
}
