using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikeLockNumber : MonoBehaviour
{
    [Header("Digit Values")]
    [Tooltip("Where this number places in the sequence")]
    public int digitPlacement;

    [Header("Rotation Values")]
    [Tooltip("The amount in degrees the object rotates when the number is changed by 1")]
    public float rotationIncrement;
    private int currentNumber = 5;
    private GameObject bikeLockParent;
    private bikeLock bikeLockScript;
    private bool currentlySelected;
    // Start is called before the first frame update
    void Start()
    {
        //References the parent object and its script
        bikeLockParent = transform.parent.gameObject;
        bikeLockScript = bikeLockParent.GetComponent<bikeLock>();
        //Matches the current value and rotation with the current code value in the parent script
        currentNumber = bikeLockScript.getCurrentCode(digitPlacement);
        transform.Rotate((-5 * rotationIncrement) + (currentNumber * rotationIncrement), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Placeholder inputs to change the rotation and number of the element
        //These controls could be changed later down the line when controls are finalised 
        if (Input.GetKeyDown("a"))
        {
            if (bikeLockScript.getSelectedElement() == digitPlacement)
            {
                if (currentNumber > 0)
                {
                    transform.Rotate(rotationIncrement, 0, 0);
                    currentNumber--;
                    //Changes the code value the parent has and then checks if the puzzle is complete
                    bikeLockScript.changeCurrentCode(digitPlacement, currentNumber);
                    bikeLockScript.checkPuzzleComplete();
                }
            }
        }
        if (Input.GetKeyDown("z"))
        {
            if (bikeLockScript.getSelectedElement() == digitPlacement)
            {
                if (currentNumber < 9)
                {
                    transform.Rotate(-rotationIncrement, 0, 0);
                    currentNumber++;
                    bikeLockScript.changeCurrentCode(digitPlacement, currentNumber);
                    bikeLockScript.checkPuzzleComplete();
                }
            }
        }
    }
}
