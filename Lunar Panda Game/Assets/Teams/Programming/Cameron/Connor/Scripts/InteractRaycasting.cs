using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycasting : MonoBehaviour
{
    Transform player;
    Transform playerCamera;
    private static InteractRaycasting _instance;
    public static InteractRaycasting Instance { get { return _instance; } }


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = Camera.main.transform;

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
        return Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist);
    }

    public bool raycastInteract(out RaycastHit hit, int layerMask)
    {
        return Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist, layerMask);
    }
}
