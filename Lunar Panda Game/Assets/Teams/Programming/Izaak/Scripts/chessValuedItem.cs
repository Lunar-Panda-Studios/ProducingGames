using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessValuedItem : MonoBehaviour
{
    [Header("Board Co-ordinates")]
    [Tooltip("The board co-ordinates where this piece needs to go to pass this part of the puzzle")]
    public Vector2 correctLocation;
    [Tooltip("The board co-ordinates at which the piece is currently stored")]
    public Vector2 currentLocation;

    private GameObject player;
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
        //This just ensures that the item held being checked by another script returns a value
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
