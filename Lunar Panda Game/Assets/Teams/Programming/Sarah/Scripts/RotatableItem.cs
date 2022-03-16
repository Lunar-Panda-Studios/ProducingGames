using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableItem : MonoBehaviour
{
    InteractRaycasting playerPickupRay;
    Transform player;

    PlayerPickup pickup;
    internal Vector3 startLocation;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
    }
}
