using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    private float jumpValue;
    public float p_speed;

    void Start()
    {
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (gameObject.GetComponent<playerJump>().getJumpStatus() == true)
        {
            
        }
        else
        {
            //jumpValue = (-1 * transform.up)
        }
        p_rigidbody.velocity = ((transform.forward * z) * p_speed) + ((transform.right * x) * p_speed) + (new Vector3 (0, p_rigidbody.velocity.y, 0));

        if (Input.GetKeyDown(KeyCode.K))
        {
            StaminaBar.instance.staminaUsage(15);
        }

    }
}
