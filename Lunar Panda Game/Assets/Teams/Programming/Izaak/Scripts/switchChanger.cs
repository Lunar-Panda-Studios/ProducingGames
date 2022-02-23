using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchChanger : MonoBehaviour
{
    //Current state of switch
    [SerializeField] bool switchMode = true;
    [Tooltip("The wires that will be used after the switch is off")]
    public GameObject amogus;
    public bool isPowerOn = false;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void changeSwitchState()
    {
        switchMode = !switchMode;
        if (switchMode && anim != null)
        {
            anim.SetTrigger("Up");
        }
        else if (anim != null)
        {
            anim.SetTrigger("Down");
        }
    }

    public bool getSwitchState()
    {
        return switchMode;
    }

    public void TurnPowerOn()
    {
        isPowerOn = true;
        //call a function from another script that handles power
    }

    public void TurnPowerOff()
    {
        isPowerOn = false;
    }
}
