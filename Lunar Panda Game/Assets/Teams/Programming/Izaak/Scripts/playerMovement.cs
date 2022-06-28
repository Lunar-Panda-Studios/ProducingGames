using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;
    [SerializeField] CapsuleCollider playerCollider; 
    public CrouchTrigger crouchTrigger;

    [Header("Move Settings")]
    [Tooltip("Speed the player moves at")]
    public float p_speed = 3f;
    public const float walkSpeed = 3.0f;
    public const float runSpeed = 5.0f;
    internal bool isSprinting;
    protected bool isCrouching = false;
    

    void Start()
    {
        //Collects the rigidbody so it can be used in code
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Collects the horizontal and forward inputs for this frame in variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move();
        sprint();
        Crouch();
    }
    void move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Sets the velocity based on these values to move the player
        p_rigidbody.velocity = ((transform.forward * z) * p_speed) + ((transform.right * x) * p_speed) + (new Vector3(0, p_rigidbody.velocity.y, 0));

        if(p_rigidbody.velocity != new Vector3(0,0,0))
        {
            UIManager.Instance.itemFade(true);
        }
        else
        {
            UIManager.Instance.itemFade(false);
        }
    }

    void sprint() 
    {
        if (Input.GetButton("Sprint"))
        {
            p_speed = runSpeed;
            isSprinting = true;
        }
        else if (true)
        {
            p_speed = walkSpeed;
            isSprinting = false;
        }
    }

    void Crouch()
    {

        //Set the key for crouch
        var crouchButton = Input.GetKey(KeyCode.LeftControl);


        if (!isCrouching && Input.GetButtonDown("Crouch"))
        {
            //Set player height to 0.5 when holding crouch key and center to 0.25
            playerCollider.height = 0.5f;
            playerCollider.center = new Vector3(playerCollider.center.x, 0.25f, playerCollider.center.z);
            isCrouching = true;
        }
        else
            if (isCrouching && Input.GetButtonDown("Crouch") && crouchTrigger.isObjectAbove == false)
        {
            //var cantStandUp = Physics.Raycast(transform.position, Vector3.up, 2f);

            //Checks if player can stand up
            //if(!cantStandUp)
            //{
            //Sets player height back to 2 and resets center back to 0
            playerCollider.height = 2f;
            playerCollider.center = new Vector3(playerCollider.center.x, 0f, playerCollider.center.z);
            isCrouching = false;
            //}
        }
    }
}