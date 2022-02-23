using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour
{
    PlayerPickup pickup;
    public ItemData data;
    internal Vector3 startLocation;


    void Awake()
    {
        pickup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickup>();
        startLocation = transform.position;
    }

    void OnCollisionEnter(Collision col)
    {
        //if the object we collided with is not the player, and if the player is holding this object
        /*
        instead of dropping the object when it hits something (to stop glitching with the items) i'm thinking of making
        the object lerp towards where the mouse is. This would hopefully stop glitching and make it so that you could
        bump the object into stuff without it dropping. I'd probably handle that in the playerpickup script though
        */
        if (col.gameObject != GameObject.FindGameObjectWithTag("Player") && pickup.heldItem == this.gameObject)
        {
            
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject != GameObject.FindGameObjectWithTag("Player") && pickup.heldItem == this.gameObject)
        {
            
        }
    }


}
