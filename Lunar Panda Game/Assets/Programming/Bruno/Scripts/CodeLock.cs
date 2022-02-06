using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLock : MonoBehaviour
{
    int codeLenght;
    int placeinCode;

    public string code = "";

    void CheckCode()
    {
        
    }

    public void SetValue(string value)
    {
        placeinCode++;

        if(placeinCode == codeLenght)
        {
            CheckCode();


        }
    }
}
