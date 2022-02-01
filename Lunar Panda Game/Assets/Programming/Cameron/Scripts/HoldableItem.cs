using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour
{

    [SerializeField] PlayerPickup pickup;

    void Awake()
    {
        pickup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
    }

    void OnCollisionEnter(Collision col)
    {
        //if the object we collided with is not the player, and if the player is holding this object
        if (col.gameObject != GameObject.FindGameObjectWithTag("Player") && pickup.heldItem == this.gameObject)
        {
            pickup.DropHeldItem();
        }
        
    }
}
