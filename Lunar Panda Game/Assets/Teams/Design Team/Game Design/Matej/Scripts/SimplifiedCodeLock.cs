using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimplifiedCodeLock : MonoBehaviour
{
    //Author: Matej Gajdos - Game Designer
    //The original script is "CodeLock" from Bruno
    //I have removed the analysis stuff as it was a lot of errors
    //Only used for the 'Food Chain' puzzle

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
        Debug.Log("The code length is " + codeLenght);
    }

    public void Update()
    {
        CodeScreen.text = attemptedCode;
    }

    void CheckCode()
    {
        if (attemptedCode == code)
        {
            StartCoroutine(Open());
            Debug.Log("Correct Code");
        }
        else
        {
            resetPuzzle();
            Debug.Log("Wrong Code");
        }
    }

    IEnumerator Open() //Rotates the door
    {
        //SoundEffectManager.GlobalSFXManager.PlaySFX(AudioClipName);       
        anim.GetComponent<Animator>().SetTrigger("Open");

        yield return new WaitForSeconds(4); 
        // In case we want something to happen write bellow
        // For example a scripted event 
    }

    public void SetValue(string value)
    {
        Debug.Log("Added value");
        placeinCode++;
        if (placeinCode <= codeLenght)
        {
            attemptedCode += value;
        }
        if (placeinCode == codeLenght)
        {
            CheckCode();

            attemptedCode = "";
            placeinCode = 0;
        }
    }

    public void resetPuzzle()
    {
        attemptedCode = "";
    }
    public void writeCode(string word) // Specific function for the 'Food Chain' puzzle
    {
        code = word;
        codeLenght = code.Length;
    }
}
