using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;

    [Header("Move Settings")]
    [Tooltip("Speed the player moves at")]
    public float stamSpeed = 12;
    public float p_speed = 5;
    public float slowSpeed = 1;
    public float lowStamThreshold = 25;
    public float runStamReq = 0.02f;

    void Start()
    {
        //Collects the rigidbody so it can be used in code
        p_rigidbody = gameObject.GetComponent<Rigidbody>();


    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Collects the horizontal and forward inputs for this frame in variables
        //Sets the velocity based on these values to move the player
        if ((runStamReq > StaminaBar.instance.currentStam) || StaminaBar.instance.currentStam < lowStamThreshold)
        {
            moveSlow();
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            moveFast();
        }
        else
        {
            move();
        }

        Debug.Log(p_rigidbody.velocity.x);
        Debug.Log(p_rigidbody.velocity.y);
    }
        void move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            p_rigidbody.velocity = ((transform.forward * z) * p_speed) + ((transform.right * x) * p_speed) + (new Vector3(0, p_rigidbody.velocity.y, 0));
        }

        void moveFast()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            p_rigidbody.velocity = ((transform.forward * z) * stamSpeed) + ((transform.right * x) * stamSpeed) + (new Vector3(0, p_rigidbody.velocity.y, 0));
            StaminaBar.instance.staminaUsage(runStamReq);
        }

        void moveSlow()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            p_rigidbody.velocity = ((transform.forward * z) * slowSpeed) + ((transform.right * x) * slowSpeed) + (new Vector3(0, p_rigidbody.velocity.y, 0));
        }
}
