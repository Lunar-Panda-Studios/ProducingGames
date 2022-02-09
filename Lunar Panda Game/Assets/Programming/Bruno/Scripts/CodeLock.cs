using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLock : MonoBehaviour
{
    int codeLenght;
    int placeinCode;

    public string code = "";
    public string attemptedCode;

    public Transform toOpen;

    public void Start()
    {
        codeLenght = code.Length;
    }


    void CheckCode()
    {
        if(attemptedCode == code)
        {
            StartCoroutine(Open());
        }
        else
        {
            Debug.Log("Wrong Code");
        }
    }

    IEnumerator Open() //Rotates the door
    {
        toOpen.Rotate(new Vector3(0, 90, 0), Space.World);        

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
}
