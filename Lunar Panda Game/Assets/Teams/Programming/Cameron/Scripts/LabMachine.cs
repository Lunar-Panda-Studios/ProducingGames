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
    [SerializeField] List<Material> startingTubeMats;
    [SerializeField] List<Material> completeTubeMats;
    [SerializeField] SwitchWall switchWall;
    [SerializeField] GameObject button;
    [SerializeField] ItemData antidoteData;
    [SerializeField] GameObject antidote;
    [SerializeField] Material antidoteGlowMat;
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
                        HoldableItem holdableItem = antidote.AddComponent<HoldableItem>();
                        holdableItem.data = antidoteData;
                        //add glowyinteractthingie
                        GlowWhenLookedAt glowy = antidote.AddComponent<GlowWhenLookedAt>();
                        glowy.glowingMaterial = antidoteGlowMat;
                        antidote.AddComponent<Rigidbody>();
                        antidote.transform.parent = null;
                    }
                }
            }
        }
    }

    //tube num has to be 0, 1, or 2
    public void TurnTubeOn(bool[] completedCombos)
    {
        //goes through the bool array of completed combos, and if any are true, change em to be "on"
        for(int i = 0; i < completedCombos.Length; i++)
        {
            if (completedCombos[i])
            {
                tubes[i].GetComponent<MeshRenderer>().material = completeTubeMats[i];
                //I think they want to play a sound here
            }
        }
    }

    public void ResetMachine()
    {
        for (int i = 0; i < tubes.Count; i++)
        {
            tubes[i].GetComponent<MeshRenderer>().material = startingTubeMats[i];
        }
    }
}
