using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodChainBrain : MonoBehaviour
{
    //Author: Matej Gajdos - Game Design

    [Header("Code & Animation")]
    public Disc X;
    public Disc L;
    public Disc M;
    public Disc S;

    [Header("Lock")]
    public SimplifiedCodeLock codeLock; 

    int firstNumber;
    int secondNumber;
    int thirdNumber;
    int fourthNumber;

    public string theCode;

    public void Start()
    {
        firstNumber = X.GetNumber();
        secondNumber = L.GetNumber();
        thirdNumber = M.GetNumber();
        fourthNumber = S.GetNumber();
        theCode = firstNumber + "" + secondNumber + "" + thirdNumber + "" + fourthNumber;
        codeLock.writeCode(theCode);
    }
    public void TurnDiscs(char size)
    {
        switch(size)
        {
            case 'X':
                X.Rotate();
                L.Rotate();
                M.Rotate();
                S.Rotate();
                break;
            case 'L':
                L.Rotate();
                M.Rotate();
                S.Rotate();
                break;
            case 'M':
                M.Rotate();
                S.Rotate();
                break;
            case 'S':
                S.Rotate();
                break;
        }
    }

}
