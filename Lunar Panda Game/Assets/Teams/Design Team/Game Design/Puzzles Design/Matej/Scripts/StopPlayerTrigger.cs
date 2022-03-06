using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerTrigger : MonoBehaviour
{
    public GameObject player;
    public float delay;
    public MovableObject movableObject;
    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            //Disable the playerMovement and reset the rigidbody velocity
            //Starts the coroutine
            player.GetComponent<playerMovement>().enabled = false;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(Coroutine(delay));
            movableObject.enabled = true;
        }
    }
    //Activating the playerMovement again after delay and destroying this trigger so
    // it wont trigger again
    private IEnumerator Coroutine(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        player.GetComponent<playerMovement>().enabled = true;
        Destroy(this);
    }

}
