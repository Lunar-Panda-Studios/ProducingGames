using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Horror : MonoBehaviour
{
    public enum HorrorEvent
    {
        DisableAtStart,
        DisableMovement,
        MoveObject,
        Teleport,
        LightOnOff,
        Jumpscare,
        LookAt,
        PlaySound,
        DropObject,
        Levitate,
        ThrowObject,
        EnableOtherTrigger
    };

    public HorrorEvent state;

    private GameObject player;

    //Disable At Start
    public bool disableAtStart;

    //Disable Movement Settings
    public float delayBeforeMovingAgain;

    //Move Object Settings
    public MovableObject movableObject;
    public float delayMoveObject;

    //Teleport Object Settings
    public MovableObject teleportObject;

    //Lights ON/OFF Settings
    public bool lightsOnOff;
    public List<Light> Lights = new List<Light>();

    //Jumpscare Settings
    public Image jumpSImage;
    public float stayOnScreenFor;

    //Look At Settings
    public GameObject camera;
    public Transform lookAt;
    public float lookAtDelay;
    public float damping;
    private Vector3 lookPos = new Vector3();
    private bool startLook;

    //Play Sound Settings
    public string clipName;

    //Drop Object Settings
    public GameObject dropObject;

    //Levitate Objects Settings
    public List<FallObject> LevitateObjects = new List<FallObject>();
    public float forceUp;
    public float forceDown;
    public float delay;

    //Throw Object Settings
    public FallObject throwObject;
    public float force;

    //Enable Other Trigger Settings
    public GameObject otherTrigger;
}
