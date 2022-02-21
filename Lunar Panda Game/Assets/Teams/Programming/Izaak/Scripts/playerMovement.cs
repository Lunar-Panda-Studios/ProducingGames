using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;

    [Header("Move Settings")]
    [Tooltip("Speed the player moves at")]
    public float p_speed = 2f;
    public float runStamReq = 0.02f;

    void Start()
    {
        //Collects the rigidbody so it can be used in code
        p_rigidbody = gameObject.GetComponent<Rigidbody>();


    }

    void Update()
    {
        //Collects the horizontal and forward inputs for this frame in variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //if (StaminaBar.instance.currentStam <= 10)
        //{
        //    p_speed = 2.0f;
        //}
        //if (Input.GetKey(KeyCode.LeftShift) && (StaminaBar.instance.currentStam > runStamReq))
        //{
        //    p_speed = 10.0f;
        //    StaminaBar.instance.staminaUsage(runStamReq);
        //}
        //else if (true)
        //{
        //    p_speed = 5.0f;        
        //}

        move();

    }
    void move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Sets the velocity based on these values to move the player
        p_rigidbody.velocity = ((transform.forward * z) * p_speed) + ((transform.right * x) * p_speed) + (new Vector3(0, p_rigidbody.velocity.y, 0));
    }
}