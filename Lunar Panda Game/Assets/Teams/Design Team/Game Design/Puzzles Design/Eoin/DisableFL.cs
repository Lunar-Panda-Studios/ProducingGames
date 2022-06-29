using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFL : MonoBehaviour
{
    Flashlight flashlight;
    public GameObject torch;
    public Light playerSpotLight;
    public Light TorchSpotLight;

    public GameObject InvTutTorch;

    bool doOnce;
    bool doOnceTwo;

    // Start is called before the first frame update
    void Start()
    {   
        flashlight = FindObjectOfType<Flashlight>();
        flashlight.enabled = false;

        doOnce = false;
        doOnceTwo = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (torch.activeSelf)
        {
            flashlight.enabled = false;
            playerSpotLight.enabled = false;
            TorchSpotLight.enabled = true;

            doOnceTwo = !doOnceTwo;
        }

        if (!torch.activeSelf)
        {
            if (!doOnce)
            {
                InvTutTorch.SetActive(true);
                doOnce = true;
            }

            flashlight.enabled = true;

            if (!doOnceTwo)
            {
                playerSpotLight.enabled = true;
                TorchSpotLight.enabled = false;
                doOnceTwo = true;
            }

            
        }
    }
}