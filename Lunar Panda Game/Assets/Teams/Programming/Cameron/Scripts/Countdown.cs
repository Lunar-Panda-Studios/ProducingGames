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
    TestingSave manager;

    void Awake()
    {
        timeLeft = countdownTime;
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        manager = FindObjectOfType<TestingSave>();
    }

    void Update()
    {
        if(timerActive)
            timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            manager.load();
            this.GetComponent<Collider>().enabled = true;
            StopTimer();
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
        timeLeft = countdownTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        print("Enter");
        if (other.gameObject.CompareTag("Player"))
        {
            StartTimer();
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
