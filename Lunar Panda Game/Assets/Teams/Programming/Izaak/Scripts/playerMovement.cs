using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody p_rigidbody;

    [Header("Move Settings")]
    [Tooltip("Speed the player moves at")]
    public float p_speed = 2f;
    public float runStamReq = 0.02f;
    public const float walkSpeed = 2.0f;
    public const float runSpeed = 5.0f;
    public Image RunningMan;
    public Image BackgroundSprite;
    public Image FillSprite;
    public StaminaBar BarOfStamina;
    void Start()
    {
        //Collects the rigidbody so it can be used in code
        p_rigidbody = gameObject.GetComponent<Rigidbody>();
        RunningMan.GetComponent<Image>();
        BarOfStamina = FindObjectOfType<StaminaBar>();


    }

    void Update()
    {
        //Collects the horizontal and forward inputs for this frame in variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //if (StaminaBar.instance.currentStam <= 10)
        //{
        //    p_speed = 2.0f;
        //}
        if (Input.GetButton("Sprint") && (StaminaBar.instance.currentStam > runStamReq) && BarOfStamina.CanSprint == true)
        {
            p_speed = runSpeed;
            StaminaBar.instance.staminaUsage(runStamReq);
        }
        else if (true)
        {
            p_speed = walkSpeed;
            RunningMan.color = new Color(0f, 0f, 0f, 1f);
            BackgroundSprite.color = new Color(0.1f, 0.25f, 0.1f, 1f);
            FillSprite.color = new Color(0f, 1f, 0f, 1f);
        }

        if (StaminaBar.instance.currentStam <= 3f && BarOfStamina.CanSprint == true)
        {
                RunningMan.color = new Color(1f, 0f, 0f, 0.1f);
            BackgroundSprite.color = new Color(0.1f, 0.25f, 0.1f, 0.1f);
            FillSprite.color = new Color(0f, 1f, 0f, 0.1f);
        }

        if (StaminaBar.instance.currentStam <= 0f && BarOfStamina.CanSprint == true)
        {
                RunningMan.color = new Color(0f, 0f, 0f, 1f);
        }

        move();

    }
    void move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Sets the velocity based on these values to move the player
        p_rigidbody.velocity = ((transform.forward * z) * p_speed) + ((transform.right * x) * p_speed) + (new Vector3(0, p_rigidbody.velocity.y, 0));
    }
}