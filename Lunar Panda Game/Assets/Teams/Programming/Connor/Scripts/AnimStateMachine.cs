using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateMachine : MonoBehaviour
{
    private Rigidbody Player;
    private enum State { idle, run, jump, falling, sprinting};
    private State state = State.idle;

    private bool walking = false;
    private bool falling = false;
    private bool jumping = false;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Debug.Log(state);
        animState();
        anim.SetInteger("State", (int)state);
    }
    private void animState()
    {

        if (Player.velocity.x < 0.05f && Player.velocity.x > -0.05f && Player.velocity.y < 0.05f && Player.velocity.y > -0.05f && Player.velocity.z < 0.05f && Player.velocity.z > -0.05f)
        {
                state = State.idle;
        }
        if (Player.velocity.y < -0.1f)
        {
            state = State.falling;
        }
        else if (state == State.falling)
        {
            if (!falling && !jumping)
            {
                state = State.idle;
            }
        }
        else if (Player.velocity.x > 0.05f || Player.velocity.x < -0.05f || Player.velocity.y > 0.05f || Player.velocity.y < -0.05f || Player.velocity.z > 0.05f || Player.velocity.z < -0.05f)
        {
            if (state != State.jump)
            {
                state = State.run;
            }
        }
        else
        {
            if ((state == State.idle || state == State.run || state == State.sprinting) && Input.GetButtonDown("Jump"))
            {
                state = State.jump;
            }
        }
    }
    //void amIWalking()
    //{
    //    Vector3 prevPosition = Player.position;

    //    if (Player.transform.position != prevPosition)
    //    {
    //        walking = true;
    //    }
    //    else
    //    {
    //        walking = false;
    //    }

    //    prevPosition = Player.transform.position;
    //}

    //void amIJumping()
    //{
    //    float prevPositionY = Player.position.y;

    //    if (Player.velocity.y < 0)
    //    {
    //        falling = true;
    //        jumping = false;
    //        Debug.Log("falling true");
    //    }
    //    else if (Player.velocity.y > 0.05f)
    //    {
    //        falling = false;
    //        jumping = true;
    //        Debug.Log("Jumping True");
    //    }

    //    prevPositionY = Player.transform.position.y;
    //}
}
