using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSelect : MonoBehaviour
{
    CodeLock codeLock;

    int reachRange = 100;
    switchChanger button;

    Transform player;
    Transform cam;

    InteractRaycasting tempSelectRay;

    private void Start()
    {
        button = FindObjectOfType<switchChanger>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;
        tempSelectRay = player.GetComponent<InteractRaycasting>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckHitObj();
        }
    }

    void CheckHitObj() //Temporary Raycast to check the buttons
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(tempSelectRay.raycastInteract(out hit))
        {
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLock>();

            if(codeLock != null)
            {
                if (button.isPowerOn)
                {
                    string value = hit.transform.name;
                    codeLock.SetValue(value);
                }
            }
        }
    }
}
