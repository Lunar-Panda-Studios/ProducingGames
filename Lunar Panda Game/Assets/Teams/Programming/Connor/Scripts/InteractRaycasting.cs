using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycasting : MonoBehaviour
{
    Transform player;
    Transform playerCamera;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = Camera.main.transform;
    }

    public bool raycastInteract(out RaycastHit hit)
    {
        return Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist);
    }
}
