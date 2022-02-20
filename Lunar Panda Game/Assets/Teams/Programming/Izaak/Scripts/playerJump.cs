using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    private Rigidbody p_rigidbody;

    [Header("Jump Settings")]
    [Tooltip("The strength of the jump")]
    public float p_speed;

    [Header("Ground Settings")]
    [Tooltip("Object at the base of player to detect ground")]
    public GameObject groundDetection;
    [Tooltip("How close the player must be to the ground to detect being on the ground")]
    public float groundDistance;
    private bool grounded;
    private bool isJumping;

    [Tooltip("The layer the game considers to be ground")]
    public LayerMask groundLayer;

    void Start()
    {
        //Collects the rigidbody so it can be used in code
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {


    }

    private bool isGrounded()
    {
        //Sets a raycast for this frame
        bool rayDown = Physics.Raycast(groundDetection.transform.position, Vector3.down, groundDistance, groundLayer);
        //Checks if the player pressed jump this frame and acts on the jump if the player is considered on the ground
        if (Input.GetButtonDown("Jump"))
        {
            if (rayDown == true)
            {
                p_rigidbody.AddForce(transform.up* p_speed, ForceMode.Impulse);  
            }
        }
        return rayDown;
    }
}

