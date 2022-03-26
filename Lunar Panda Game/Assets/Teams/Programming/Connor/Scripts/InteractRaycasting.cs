using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycasting : MonoBehaviour
{
    Transform player;
    Flashlight flashlight;
    Transform playerCamera;
    private static InteractRaycasting _instance;
    public static InteractRaycasting Instance { get { return _instance; } }


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = Camera.main.transform;
        flashlight = FindObjectOfType<Flashlight>();
        //flashlight.enabled = false;

        //setting up singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public bool raycastInteract(out RaycastHit hit)
    {
        Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist);

        if (hit.transform != null)
        {
            if (flashlight.enabled)
            {
                return true;
            }
            else
            {
                if (!hit.transform.CompareTag("Flashlight") && !hit.transform.CompareTag("BorisBox"))
                {
                    return false;
                }
                else
                {
                    if(hit.transform.CompareTag("Flashlight"))
                    {
                        flashlight.enabled = true;
                        hit.transform.gameObject.SetActive(false);
                    }
                    return true;
                }
            }
        }
        else
        {
            return false;
        }
    }

    public bool raycastInteract(out RaycastHit hit, int layerMask)
    {
        Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist, layerMask);
        if(flashlight.enabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
