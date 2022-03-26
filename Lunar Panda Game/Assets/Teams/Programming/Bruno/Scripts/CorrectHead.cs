using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectHead : MonoBehaviour
{
    [Header("Hooks")]
    [Tooltip("This is the empty game object where the heads will be placed")]
    public GameObject[] checker;

    [Header("Interaction")]
    [Tooltip("Currently an interaction for a door opening")]
    public Transform toOpen;
    [Tooltip("Just the soundclip")]
    public string AudioClipName;
        
    [SerializeField] bool switchMode = true;
    Animator anim;
    public string leverSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    changeSwitchState();
                    correctCheck();
                }
            }
        }
    }

    void correctCheck()
    {
        for (int i = 0; i < checker.Length; i++)
        {
            if (checker[i].GetComponent<HeadPlacing>().correctHead != true)
            {
                return;
            }    
        }

        if (Analysis.current != null)
        {
            if (Analysis.current.consent)
            {
                Analysis.current.resetTimer("Arrangement");
            }
        }
        Debug.Log("It can be opened");
        StartCoroutine(Open());
    }

    IEnumerator Open() //Rotates the door
    {
        //SoundEffectManager.GlobalSFXManager.PlaySFX(AudioClipName);
        toOpen.Rotate(new Vector3(0, 90, 0), Space.Self);

        yield return new WaitForSeconds(4); //In case we want something to happen after uncomment bellow 

        //toOpen.Rotate(new Vector3(0, -90, 0), Space.World);
    }

    public void changeSwitchState()
    {
        switchMode = !switchMode;
        if (switchMode && anim != null)
        {
            anim.SetTrigger("Up");
            if (SoundEffectManager.GlobalSFXManager != null && leverSound != null)
            {
                SoundEffectManager.GlobalSFXManager.PlaySFX(leverSound);
            }
        }
        else if (anim != null)
        {
            anim.SetTrigger("Down");
            if (SoundEffectManager.GlobalSFXManager != null && leverSound != null)
            {
                SoundEffectManager.GlobalSFXManager.PlaySFX(leverSound);
            }
        }
    }
}
