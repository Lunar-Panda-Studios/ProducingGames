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
    GameManager manager;
    InteractRaycasting ray;
    Inventory inventoryScript;
    [SerializeField] ItemData antidoteData;

    void Awake()
    {
        timeLeft = countdownTime;
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        manager = FindObjectOfType<GameManager>();
        inventoryScript = FindObjectOfType<Inventory>();
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

        //if (Input.GetButtonDown("Interact"))
        //{
        //    RaycastHit hit;
        //    InteractRaycasting.Instance.raycastInteract(out hit);
        //    if (hit.transform.gameObject != null && hit.transform.gameObject == gameObject)
        //    {
        //        if (inventoryScript.itemInventory[inventoryScript.selectedItem] == antidoteData)
        //        {
        //            StartCoroutine(UIManager.Instance.FadePanelIn());
        //        }
        //    }
        //}
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
