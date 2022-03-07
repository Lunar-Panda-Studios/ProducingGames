using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{
    [SerializeField] public Animator doorAnim = null;

    private bool doorOpen = false;

    [SerializeField] public string OpenAnimationName = "DoorOpen";
    [SerializeField] public string CloseAnimationName = "DoorClose";

    [SerializeField] public int waitTimer = 1;

    [SerializeField] public bool pauseInteraction = false;

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

        }
        else if (doorOpen && !pauseInteraction)
        {
            doorAnim.Play(CloseAnimationName, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());

        }
    }



}


