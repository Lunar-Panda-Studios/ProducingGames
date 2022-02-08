using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchChanger : MonoBehaviour
{
    //Current state of switch
    private bool switchMode = true;
    [Tooltip("The wires that will be used after the switch is off")]
    public GameObject amogus;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //If the wires part of the puzzle is solved
            if (amogus.GetComponent<ConnectInputsAndOutputs>().CheckCombination() == true)
            {
                switchMode = true;
            }
            else
            {
                switchMode = false;
            }
        }
    }

    public void changeSwitchState()
    {
        switchMode = !switchMode;
    }

    public bool getSwitchState()
    {
        return switchMode;
    }
}
