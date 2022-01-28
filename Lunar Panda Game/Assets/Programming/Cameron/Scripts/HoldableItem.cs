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
        if (col.gameObject != GameObject.FindGameObjectWithTag("Player"))
        {
            pickup.DropHeldItem();
        }
        
    }
}
