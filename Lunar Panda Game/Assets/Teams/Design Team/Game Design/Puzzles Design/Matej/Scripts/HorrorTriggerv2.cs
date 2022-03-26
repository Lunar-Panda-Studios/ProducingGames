using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorrorTriggerv2 : MonoBehaviour
{
    //List of all of the events added
    public List<HorrorData> Events = new List<HorrorData>();

    private int check;//This is the main logic for this all to work. !FRAGILE!

    private void OnTriggerEnter(Collider other)//Trigger duh
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(HorrorData data in Events)//Every event in the list of 
            {
                StartTrigger(data);
            }
            if (check == Events.Count) Destroy(this.gameObject);//Checks if all of the events are completed, then delete the trigger
        }
    }
    public void StartTrigger(HorrorData data)// This activates the trigger depending on the type of the trigger
    {
        switch (data.type)
        {

           default: break;
        }
    }
    //Coroutines need to be done here as the HorrorData class does not inherit the MonoBehaviour
    //The class inheriting from the MonoBehaviour messed stuff up, this makes it a bit harder to understand...

    //Couroutines

}
[System.Serializable]
public enum HorrorEvent//Types of the horror events
{
    DisablePlayerMovement,
    MoveObject,
    TeleportObject,
    LightsOnOff,
    Jumpscare,
    LookAt,
    PlaySound,
    StopSound,
    DropObject,
    LevitateObjects,
    ThrowObject,
    ActivateAnotherTrigger
}
[System.Serializable]
public class HorrorData  //Data for all of the classes, bear in mind that each only uses some of the variables
{
    //Variables

    public HorrorEvent type;
    private GameObject player;
    //Rotating camera to the mirror settings
    public GameObject camera;

    public bool disableAtStart;
    public AudioSource audioSource;

    [Header("---DISABLE MOVEMENT SETTINGS---")]
    public bool disablePlayerMovement;
    public float delayBeforeMovingAgain;

    [Header("---MOVE OBJECT SETTINGS---")]
    public bool move;
    public MovableObject movableObject;
    public float delayMoveObject;

    [Header("---TELEPORT OBJECT SETTINGS---")]
    public bool teleport;
    public MovableObject teleportObject;


    [Header("---LIGHTS ON/OFF SETTINGS---")]
    public bool lights;
    public bool lightsOnOff;
    public List<Light> Lights = new List<Light>();

    [Header("---JUMPSCARE SETTINGS---")]
    public bool jump;
    public Image jumpSImage;
    public float stayOnScreenFor;

    [Header("---LOOK AT SETTINGS---")]
    public bool look;
    public Transform lookAt;
    public float lookAtDelay;
    public float damping;
    private Vector3 lookPos = new Vector3();
    private bool startLook;

    [Header("---PLAY SOUND SETTINGS---")]
    public bool play;
    public string clipName;

    [Header("---STOP SOUND SETTINGS---")]
    public bool stop;
    public string stopClipName;

    [Header("---DROP OBJECT SETTINGS---")]
    public bool drop;
    public GameObject dropObject;

    [Header("---LEVITATE OBJECTS SETTINGS---")]
    public bool levitate;
    public List<FallObject> LevitateObjects = new List<FallObject>();
    public float forceUp;
    public float forceDown;
    public float delay;

    [Header("---THROW OBJECT SETTINGS---")]
    public bool throW;
    public FallObject throwObject;
    public float force;

    [Header("---ENABLE OTHER TRIGGER SETTINGS---")]
    public bool enableOtherTrigger;
    public HorrorTrigger otherTrigger;

    //Functions
    public void PlaySound(string clipName)
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(clipName);
    }
    public void StopSound(string clipName)
    {
        SoundEffectManager.GlobalSFXManager.PauseSFX(clipName);
    }
    public void DropObject()
    {
        dropObject.GetComponent<Rigidbody>().useGravity = true;
    }
    public void Levitate()
    {
        foreach (FallObject levitateObject in LevitateObjects)
        {
            levitateObject.Levitate(forceUp, forceDown, delay);
        }
    }
    public void ThrowObject(float force)
    {
        throwObject.Fly(force);
    }
    public void TestFunction(string[] test)
    {

    }
}
