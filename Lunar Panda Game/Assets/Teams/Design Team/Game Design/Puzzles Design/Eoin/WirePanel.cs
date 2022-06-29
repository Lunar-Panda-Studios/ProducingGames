using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePanel : MonoBehaviour
{
    [SerializeField]
    GameObject doorPivot;

    public string AudioClipName;
    public bool DoOnce;

    private void Start()
    {
        DoOnce = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (!DoOnce)
                    {
                        doorPivot.GetComponent<Animation>().Play();
                        StartCoroutine(OpenPannel());
                    }
                }
            }
        }
    }
    IEnumerator OpenPannel() //Rotates the door
    {
        DoOnce = true;
        SoundEffectManager.GlobalSFXManager.PlaySFX(AudioClipName);

        yield return new WaitForSeconds(4); //In case we want something to happen after uncomment bellow 
    }

    //public void PlaySound()
    //{
    //    SoundEffectManager.GlobalSFXManager.PlaySFX(AudioClipName);
    //}

}
