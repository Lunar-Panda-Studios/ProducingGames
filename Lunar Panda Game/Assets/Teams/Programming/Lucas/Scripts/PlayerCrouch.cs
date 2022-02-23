using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    protected CapsuleCollider playerCollider;
    
    protected bool isCrouching = false;

    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Crouch();
    }

    private void Crouch()
    {

        //Set the key for crouch
        var crouchButton = Input.GetKey(KeyCode.LeftControl);

       
        if(!isCrouching && Input.GetButtonDown("Crouch"))
        {
            //Set player height to 0.5 when holding crouch key and center to 0.25
            playerCollider.height = 0.5f;
            playerCollider.center = new Vector3(playerCollider.center.x, 0.25f, playerCollider.center.z);
            isCrouching = true;
        }

        if(isCrouching && !Input.GetButton("Crouch"))
        {
            var cantStandUp = Physics.Raycast(transform.position, Vector3.up, 2f);

            //Checks if player can stand up
            if(!cantStandUp)
            {
                //Sets player height back to 2 and resets center back to 0
                playerCollider.height = 2f;
                playerCollider.center = new Vector3(playerCollider.center.x, 0f, playerCollider.center.z);
                isCrouching = false;
            }
        }
    }
}
