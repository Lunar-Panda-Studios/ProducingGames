using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverUP : MonoBehaviour
{
    // A copy from Connor's Door Open/Close Script


    public int id;
    [Tooltip("Assign key for opening and closing")]
    public KeyCode openCloseKey;
    public Animator anim;
    [Tooltip("Set this to time of objects opening and closing time.")]
    public float animTimer = 0.5f;
    bool animDone = true;   

    private bool isOpen = false;

    void Start()
    {
        //GameEvents.current.triggerCloseDoor += openCloseObject;  Needs updating for the Lever
        //GameEvents.current.triggerOpenDoor += openCloseObject;
    }


    void Update()
    {
        if (Input.GetKeyDown(openCloseKey) /*&& animDone*/) // press key and it opens or closes object (add to camerons script when merged)
        {
            openCloseObject(id);
        }
    }

    public void openCloseObject(int id)
    {
        if(id == this.id)
        {
            switch (isOpen) // checks to see if you're opening or closing
            {
                case false:
                    isOpen = true;
                    anim.SetTrigger("Down");
                    break;

                case true:
                    isOpen = false;
                    anim.SetTrigger("Up");
                    break;
            }
        }

        //animWait();
    }    
}