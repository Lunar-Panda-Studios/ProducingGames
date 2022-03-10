using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenClose : MonoBehaviour
{
    internal int id;
    public Animator anim;
    [Tooltip("Set this to time of objects opening and closing time.")]
    public float animTimer = 0.5f;
    bool animDone = true;   

    private bool isOpen = false;

    void Start()
    {
        GameEvents.current.triggerCloseDoor += openCloseObject;
        GameEvents.current.triggerOpenDoor += openCloseObject;
    }


    void Update()
    {
        //if (Input.GetKeyDown(openCloseKey) /*&& animDone*/) // press key and it opens or closes object (add to camerons script when merged)
        //{
        //    openCloseObject(id);
        //}
    }

    public void openCloseObject(int id)
    {
        if(id == this.id)
        {
            switch (isOpen) // checks to see if you're opening or closing
            {
                case false:
                    isOpen = true;
                    anim.SetTrigger("Open");
                    break;

                case true:
                    isOpen = false;
                    anim.SetTrigger("Close");
                    break;
            }
        }

        //animWait();
    }
    //Code waiting between animations that failed miserably
    //IEnumerator animWait()
    //{
    //    animDone = false;
    //    yield return new WaitForSeconds(animTimer);
    //    animDone = true;
    //}
}