using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAttach : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        //The camera's position is snapped onto the player's on every frame
        transform.position = player.transform.position;
    }
}
