using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    public string rotateAudio;//Matej edit
    public string placeAudio;//Matej edit
    public string openAudio;//Matej edit
    bool handsConnected = false;
    const float clockAngle = 30f;
    const float hourAngle = 90;
    [Header("Prefabs")]
    [Tooltip("This should be the prefab of the Hour hand")]
    [SerializeField] GameObject hourHand;
    [Tooltip("This should be the prefab of the Minute hand")]
    [SerializeField] GameObject minuteHand;
    [Tooltip("This should be the prefab of the clock hands")]
    [SerializeField] GameObject clockHands;
    [Header("Data")]
    [Tooltip("This should be the item data of the clock hands")]
    [SerializeField] ItemData clockHandsData;
    Inventory inventory;
    [Header("Puzzle Completion")]
    [Tooltip("Put the angle the minute hand needs to be in (doesn't have to be super exact, can be 1 degree off)")]
    [SerializeField] float desiredMinuteRotation;
    [Tooltip("Put the angle the hour hand needs to be in (doesn't have to be super exact, can be 1 degree off)")]
    [SerializeField] float desiredHourRotation;
    [Header("Temp")]
    [SerializeField] Transform door;
    [SerializeField] Transform pivot;
    private bool completed;

    /*[Tooltip("This is the position of the minute hand when its placed on the clock")]
    [SerializeField] Vector3 minHandClockPos;
    [Tooltip("This is the position of the hour hand when its placed on the clock")]
    [SerializeField] Vector3 hourHandClockPos;*/

    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if(InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (handsConnected)
                    {
                        RotateHand();
                        SoundEffectManager.GlobalSFXManager.PlaySFX(rotateAudio);//Matej edit
                    }
                    else
                    {
                        PlaceHands();
                        SoundEffectManager.GlobalSFXManager.PlaySFX(placeAudio);//Matej edit
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    void RotateHand()
    {
        //minuteHand.transform.eulerAngles = new Vector3(minuteHand.transform.eulerAngles.x, minuteHand.transform.eulerAngles.y, minuteHand.transform.eulerAngles.z + clockAngle);
        minuteHand.transform.RotateAround(transform.position, Vector3.forward, clockAngle);
        //the issue with just doing if minute hand is at 90 degrees is that float precision is a piece
        if(minuteHand.transform.eulerAngles.z > hourAngle - 1 && minuteHand.transform.eulerAngles.z < hourAngle + 1)
        {
            hourHand.transform.RotateAround(transform.position, Vector3.forward, clockAngle);
        }
        CheckCombination();
    }

    void PlaceHands()
    {
        if(inventory.itemInventory[inventory.selectedItem] == clockHandsData)
        {
            clockHands = Instantiate(clockHands, transform.position, Quaternion.identity);
            minuteHand = clockHands.transform.GetChild(0).gameObject;
            Destroy(minuteHand.GetComponent<Rigidbody>());
            Destroy(minuteHand.GetComponent<GlowWhenLookedAt>());
            Destroy(minuteHand.GetComponent<HoldableItem>());
            Destroy(minuteHand.GetComponent<Collider>());
            hourHand = clockHands.transform.GetChild(1).gameObject;
            Destroy(hourHand.GetComponent<Rigidbody>());
            Destroy(hourHand.GetComponent<GlowWhenLookedAt>());
            Destroy(hourHand.GetComponent<HoldableItem>());
            Destroy(hourHand.GetComponent<Collider>());
            //remove the current selected item (clock hands) from the inventory
            inventory.removeItem();
            handsConnected = true;
            if (FindObjectOfType<PlayerPickup>().heldItem != null)
            {
                if (FindObjectOfType<PlayerPickup>().heldItem.name == "Hand_Min" || FindObjectOfType<PlayerPickup>().heldItem.name == "Hand_Hour")
                {
                    //idk why this is an issue, but it is. Maybe a bug with the player pickup script? or inventory script? or this script? No clue tbh
                    FindObjectOfType<PlayerPickup>().heldItem = null;
                }
            }


        }
    }

    void CheckCombination()
    {
        //if both the minute and hour hand are at the desired rotation
        if (minuteHand.transform.eulerAngles.z > desiredMinuteRotation - 1 && minuteHand.transform.eulerAngles.z < desiredMinuteRotation + 1)
        {
            if (hourHand.transform.eulerAngles.z > desiredHourRotation - 1 && hourHand.transform.eulerAngles.z < desiredHourRotation + 1)
            {
                //unlock the door
                //need to replace this with proper door unlock code stuff
                SoundEffectManager.GlobalSFXManager.PlaySFX(openAudio);//Matej edit
                door.eulerAngles = new Vector3(-90, 180, 90);//im not making this a variable as its only temporary
                door.RotateAround(pivot.position, Vector3.up, -90);
                completed = true;
            }
        }
        else
        {
            //lock the door
            if (completed)
            {
                SoundEffectManager.GlobalSFXManager.PlaySFX(openAudio);//Matej edit
                door.RotateAround(pivot.position, Vector3.up, 90);
                completed = false;
            }
            else
                door.eulerAngles = new Vector3(-90, 180, 90);
        }
    }
}
