using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSlidingDoors : MonoBehaviour
{
    bool move;
    public GameObject MoveTo;
    Vector3 startLocation;
    public float speed = 3;
    Vector3 target;
    internal int id;
    public bool startOpen;
    bool first = true;


    private void Start()
    {
        startLocation = transform.position;
        move = false;
        if(startOpen)
        {
            transform.position = MoveTo.transform.position;
            target = startLocation;
        }
        else
        {
            transform.position = startLocation;
            target = MoveTo.transform.position;
        }

        GameEvents.current.triggerOpenDoor += moveDoor;
        GameEvents.current.triggerCloseDoor += moveDoor;
    }

    private void Update()
    {
        if (move)
        {
            if (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                if (target == transform.position)
                {
                    move = false;
                }
            }

        }
    }

    void moveDoor(int id)
    {
        if(id == this.id)
        {
            changeTarget();
            move = true;
        }
    }

    void changeTarget()
    {
        move = false;
        if (!first)
        {
            if (target == startLocation)
            {
                target = MoveTo.transform.position;
            }
            else
            {
                target = startLocation;
            }
        }
        else
        {
            first = false;
        }
    }
}
