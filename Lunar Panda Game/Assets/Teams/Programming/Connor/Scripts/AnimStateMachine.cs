using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateMachine : MonoBehaviour
{
    private Rigidbody Player;
    private enum State { idle, run, jump, falling};
    private State state = State.idle;

    private bool walking = false;
    private bool falling = false;
    private bool jumping = false;
    
    void Start()
    {
        Player = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Debug.Log(state);
    }
    private void animState()
    {
        if (!falling && !jumping)
        {
            if (state == State.jump && Player.velocity.y <= 0)
            {
                state = State.idle;
            }
        }
        if (falling || jumping)
        {
            if (Player.velocity.y < 0)
            {
                state = State.falling;

            }
        }
        else if (state == State.falling)
        {
            if (!falling && !jumping)
            {
                state = State.idle;
            }
        }
        else if (walking)
        {
            if (state != State.jump)
            {
                state = State.run;
            }
        }
        else
        {
            if (state != State.jump)
            {
                state = State.idle;
            }
        }
    }
    void amIWalking()
    {
        Vector3 prevPosition = Player.position;

        if (Player.transform.position != prevPosition)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        prevPosition = Player.transform.position;
    }

    void amIJumping()
    {
        float prevPositionY = Player.position.y;

        if (Player.transform.position.y != prevPositionY && Player.transform.position.y < prevPositionY)
        {
            falling = true;
            jumping = false;
        }
        else
        {
            falling = false;
            jumping = true;
        }

        prevPositionY = Player.transform.position.y;
    }
}
