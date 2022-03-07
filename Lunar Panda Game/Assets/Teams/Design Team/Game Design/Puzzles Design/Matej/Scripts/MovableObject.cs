using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovableObject : MonoBehaviour
{
    public GameObject endObject;
    public GameObject teleportObject;
    private Vector3 endPosition = new Vector3();
    private Vector3 teleportPosition = new Vector3();
    public float speed;
    public float smoothTime;
    public bool disableAfterMove;
    Vector3 velocity;

    private bool isMoving;
    private void Start()
    {
        endPosition = endObject.transform.position;
        teleportPosition = teleportObject.transform.position;
    }
    public void Update()
    {
        if (GetIsMoving()) Move();
    }
    public void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref velocity, smoothTime, speed);
    }
    public void SetIsMoving(bool value)
    {
        isMoving = value;
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    public void Teleport()
    {
        transform.position = teleportPosition;
        transform.rotation = teleportObject.transform.rotation;
    }
    public bool GetDisableAM()
    {
        return disableAfterMove;
    }
}
