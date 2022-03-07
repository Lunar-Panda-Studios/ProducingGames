using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorrorTrigger : MonoBehaviour
{
    private GameObject player;
    public bool disableAtStart;
    public TypeOfTrigger type;

    [Header("Disable Movement")]
    public bool disableMovement;
    public float delayBeforeMovingAgain;

    [Header("Move object settings")]
    public MovableObject movableObject;
    public float delayMove;

    [Header("Lights out settings")]
    public List<Light> Lights = new List<Light>();

    [Header("Jumpscare settings")]
    public Image jumpSImage;
    public float stayOnScreenFor;

    [Header("Enable other trigger")]
    public bool enableOtherTrigger;
    public HorrorTrigger otherTrigger;

    public enum TypeOfTrigger
    {
        Move,
        Teleport,
        LightsOut,
        Jumpscare
    }
    
    public void Start()
    {
        if (disableAtStart) ToggleTriggerCollider();
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (disableMovement) DisablePlayerMovement();
            switch (type)
            {
                case TypeOfTrigger.Move:
                    Move();
                    break;
                case TypeOfTrigger.Teleport:
                    Teleport();
                    break;
                case TypeOfTrigger.LightsOut:
                    LightsOut();
                    break;
                case TypeOfTrigger.Jumpscare:
                    Jumpscare();
                    break;
                default:
                    break;
            }
            if (enableOtherTrigger) otherTrigger.ToggleTriggerCollider();
        }
    }
    
    private IEnumerator PlayerMovementCoroutine(float Delay)
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(Delay);
        player.GetComponent<playerMovement>().enabled = true;
        Destroy(this);
    }
    private IEnumerator StopMoveObject(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        movableObject.SetIsMoving(false);
        if (movableObject.GetDisableAM()) movableObject.gameObject.SetActive(false);
        Destroy(this);
    }
    private IEnumerator JumpscareStayOnScreen(float time)
    {
        //Activates the image in Canva and then disables it
        jumpSImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        jumpSImage.gameObject.SetActive(false);
    }
    public void DisablePlayerMovement()
    {
        player.GetComponent<playerMovement>().enabled = false;
        StartCoroutine(PlayerMovementCoroutine(delayBeforeMovingAgain));
    }
    public void Move()
    {
        //Disable the playerMovement and reset the rigidbody velocity
        //Starts the coroutine
        movableObject.gameObject.SetActive(true);
        StartCoroutine(StopMoveObject(delayMove));
        movableObject.SetIsMoving(true);
    }
    public void Teleport()
    {
        //Basically just telport the movable object
        //Design-vise make sure the player cannot see the transport
        movableObject.Teleport();
    }
    public void LightsOut()
    {
        foreach (Light light in Lights)
        {
            light.enabled = false;
        }
    }
    public void Jumpscare()
    {
        StartCoroutine(JumpscareStayOnScreen(stayOnScreenFor));
    }
    public void ToggleTriggerCollider()
    {
        this.gameObject.GetComponent<MeshCollider>().enabled = 
           !this.gameObject.GetComponent<MeshCollider>().enabled;
    }
}
