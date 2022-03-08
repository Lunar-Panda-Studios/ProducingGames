using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{
    public Animator doorAnim = null;
    public Animator doorAnim2 = null;

    private bool doorOpen = false;

     public string OpenAnimationName = "DoorOpen";
     public string CloseAnimationName = "DoorClose";

     public int waitTimer = 1;

    public bool pauseInteraction = false;

    private IEnumerator PauseDoorInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }

    public void PlayAnimation()
    {
        if(!doorOpen && !pauseInteraction)
        {
            doorAnim.Play(OpenAnimationName, 0, 0.0f);
            doorOpen = true;
            StartCoroutine(PauseDoorInteraction());

            doorAnim2.Play(OpenAnimationName, 0, 0.0f);
            doorOpen = true;
            StartCoroutine(PauseDoorInteraction());
        }
        else if (doorOpen && !pauseInteraction)
        {
            doorAnim.Play(CloseAnimationName, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());

            doorAnim2.Play(CloseAnimationName, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());

        }
    }



}


