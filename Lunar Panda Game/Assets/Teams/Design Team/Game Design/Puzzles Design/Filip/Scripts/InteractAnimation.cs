using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAnimation : MonoBehaviour
{
    [SerializeField] string audioClipName;

    [SerializeField] Animator animator;

    [SerializeField] bool closed = true;
    [SerializeField] float waitFor;

    bool canInteract = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (canInteract)
            {
                StartCoroutine(playAnimation());
            }
        }
    }

    IEnumerator playAnimation()
    {
        canInteract = false;
        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
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

    public void playAnimationSound()
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(audioClipName);
    }

}

