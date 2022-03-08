using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessValuedItem : MonoBehaviour
{
    public Vector2 correctLocation;
    public Vector2 currentLocation;

    public GameObject player;
    private float maxTime = 0.1f;
    private float maxClock = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerPickup>().heldItem == gameObject)
        {
            maxClock = maxTime;
        }
        else if (maxClock > 0)
        {
            maxClock -= Time.deltaTime;
        }
    }

    public float checkIfRecent()
    {
        return maxClock;
    }

    public void changeCurrentLocation(Vector2 newSpot)
    {
        currentLocation = newSpot;
    }

    public bool checkCorrectSpot()
    {
        if (currentLocation == correctLocation)
        {
            return true;
        }
        return false;
    }
}
