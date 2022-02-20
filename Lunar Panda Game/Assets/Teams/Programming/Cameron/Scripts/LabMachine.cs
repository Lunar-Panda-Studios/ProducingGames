/*
For this script i need to make it so that when one combination on the switch wall is completed,
the tube thingies change material or light up or something. Should do it in this script to make
it more object oriented, but idk if i can be bothered or if its practical to do so
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMachine : MonoBehaviour
{
    [SerializeField] List<GameObject> tubes;
    [SerializeField] SwitchWall switchWall;
    [SerializeField] GameObject button;
    Transform cam;
    Transform player;

    void Awake()
    {
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                if (hit.transform.gameObject == button)
                {
                    //if all the combos have been put in and are fine.
                    if (switchWall.CheckAllCombos())
                    {
                        //change the antidote so that it can be picked up and stuff
                        //add holdableitem and item data
                        //add glowyinteractthingie
                        //
                    }
                }
            }
        }
    }
}
