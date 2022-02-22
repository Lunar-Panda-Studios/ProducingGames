using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    bool timerActive = false;
    float timeLeft;
    [SerializeField] float countdownTime;
    [SerializeField] bool resetLevelOnCountdownEnd;
    Transform cam;
    Transform player;

    void Awake()
    {
        timeLeft = countdownTime;
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(timerActive)
            timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            //reset room/level/shit
            StopTimer();
        }

        //please i need that raycast code i hate this xd
        RaycastHit hit;
        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                StartTimer();
            }
        }
    }

    public float TimeLeft()
    {
        return timeLeft;
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    
}
