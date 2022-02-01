using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    public GameObject groundDetection;
    public float p_speed;
    public float groundDistance;
    private bool grounded;
    private bool isJumping;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool rayDown = Physics.Raycast(groundDetection.transform.position, Vector3.down, groundDistance, groundLayer);
        if (Input.GetButtonDown("Jump"))
        {

            if (rayDown == true)
            {
                print("Jump");
                //p_rigidbody.velocity = new Vector3(p_rigidbody.velocity.x, p_speed + p_rigidbody.velocity.y, p_rigidbody.velocity.z);
                p_rigidbody.AddForce(transform.up * p_speed, ForceMode.Impulse);  
            }
        }
    }

    public bool getJumpStatus()
    {
        return isJumping;
    }
}
