using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool closed = true;
    [SerializeField] float waitFor;
    Animator anim;
    [SerializeField] string InteractSound = "Electric_Switch";

    bool canInteract = true;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (canInteract && !AnimatorIsPlaying())
            {
                StartCoroutine(playAnimation());
            }
        }
    }

    bool AnimatorIsPlaying()
    {

        if (anim != null)
        {
            print(animator.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return animator.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
        else
            return false;
    }

    IEnumerator playAnimation()
    {
        canInteract = false;
        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                SoundEffectManager.GlobalSFXManager.PlaySFX(InteractSound);
                if (closed)
                {
                    animator.SetTrigger("Open");
                    closed = false;
                }
                else
                {
                    animator.SetTrigger("Close");
                    closed = true;
                }
            }
        }
        yield return new WaitForSeconds(waitFor);
        canInteract = true;
    }
}

