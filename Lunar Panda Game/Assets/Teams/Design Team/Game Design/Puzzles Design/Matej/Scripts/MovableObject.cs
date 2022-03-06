using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public GameObject endObject;
    private Vector3 endPosition = new Vector3();
    public float speed;
    public float smoothTime;
    Vector3 velocity;
    private void Start()
    {
        endPosition = endObject.transform.position;
        this.enabled = false;
    }
    public void Update()
    {
        Move();
    }
    public void Move()
    {    
        transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref velocity, smoothTime, speed); 
    }
}
