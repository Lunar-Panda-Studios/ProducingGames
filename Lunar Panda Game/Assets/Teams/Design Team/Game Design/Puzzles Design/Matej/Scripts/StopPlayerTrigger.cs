using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerTrigger : MonoBehaviour
{
    public GameObject player;
    public float delay;
    public MovableObject movableObject;
    public enum TypeOfTrigger
    {
        Move,
        Teleport
    }
    public TypeOfTrigger type;
    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            switch(type)
            {
                case TypeOfTrigger.Move:
                    //Disable the playerMovement and reset the rigidbody velocity
                    //Starts the coroutine
                    player.GetComponent<playerMovement>().enabled = false;
                    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    StartCoroutine(Coroutine(delay));
                    movableObject.SetIsMoving(true);
                    break;
                case TypeOfTrigger.Teleport:
                    //Basically just telport the movable object
                    //Design-vise make sure the player cannot see the transport
                    movableObject.Teleport();
                    break;
                default:
                    break;
            }
            
            
        }
    }
    //Activating the playerMovement again after delay and destroying this trigger so
    // it wont trigger again
    private IEnumerator Coroutine(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        player.GetComponent<playerMovement>().enabled = true;
        movableObject.SetIsMoving(false);
        Destroy(this);
    }

}
