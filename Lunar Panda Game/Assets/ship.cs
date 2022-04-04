using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship : MonoBehaviour
{
    public Transform endPosition;
    public float speed;
    public bool move;
    Vector3 velocity;
    public float smoothTime;
    private void Update()
    {
        if(move)
        {
            if(transform.position != endPosition.position)
            {
                transform.position = Vector3.SmoothDamp(transform.position, endPosition.position, ref velocity, smoothTime, speed);
            }
            else move = false;
        }
    }
    public void MoveShip()
    {
        move = true;
    }
}
