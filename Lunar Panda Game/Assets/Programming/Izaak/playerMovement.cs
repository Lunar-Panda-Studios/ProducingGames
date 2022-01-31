using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    public float p_speed;

    void Start()
    {
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(p_speed * Time.deltaTime * (Input.GetAxis("Horizontal")), 0, 0);
        }
        if (Input.GetButton("Vertical"))
        {
            transform.Translate(0, 0, p_speed * Time.deltaTime * (Input.GetAxis("Vertical")));
        }
    }
}
