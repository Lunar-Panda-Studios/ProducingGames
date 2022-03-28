using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;

    [Header("Move Settings")]
    [Tooltip("Speed the player moves at")]
    public float p_speed = 3f;
    public const float walkSpeed = 3.0f;
    public const float runSpeed = 5.0f;
    internal bool isSprinting;

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


        move();
        sprint();
       
    }
    void move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Sets the velocity based on these values to move the player
        p_rigidbody.velocity = ((transform.forward * z) * p_speed) + ((transform.right * x) * p_speed) + (new Vector3(0, p_rigidbody.velocity.y, 0));

        if(p_rigidbody.velocity != new Vector3(0,0,0))
        {
            UIManager.Instance.itemFade(true);
        }
        else
        {
            UIManager.Instance.itemFade(false);
        }
    }

    void sprint() 
    {
        if (Input.GetButton("Sprint"))
        {
           
            p_speed = runSpeed;
            isSprinting = true;
        }
        else if (true)
        {
            
            p_speed = walkSpeed;
            isSprinting = false;
        }
    }
}