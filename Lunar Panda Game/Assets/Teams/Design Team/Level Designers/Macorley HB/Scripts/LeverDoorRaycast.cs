using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorRaycast : MonoBehaviour
{
    [SerializeField] public int rayLength = 5;
    [SerializeField] public LayerMask layerMaskInteract;
    [SerializeField] public string excludeLayerName = null;

    private LeverDoorController raycastedObj;

    [SerializeField] public KeyCode openDoorKey = KeyCode.Mouse0;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "DoorLever";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if(hit.collider.CompareTag(interactableTag))
            {
                if(!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<LeverDoorController>();
                    //CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;

                if(Input.GetKeyDown(openDoorKey))
                {
                    raycastedObj.PlayAnimation();

                }
            }
        }


        else
        {
            if(isCrosshairActive)
            {
                //CrosshairChange(false);
                doOnce = false;
            }
        }

    }

   // void CrosshairChange(bool on)
   // {
       // if (on && !doOnce)
        //{
           // CrosshairChange.color = Color.red;
        //}
       // else
       // {
           // CrosshairChange.color = Color.white;
           // isCrosshairActive = false;
        //}
   // }

}
