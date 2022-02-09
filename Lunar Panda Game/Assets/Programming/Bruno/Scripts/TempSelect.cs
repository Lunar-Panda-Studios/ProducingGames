using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSelect : MonoBehaviour
{
    CodeLock codeLock;

    int reachRange = 100;
    switchChanger button;

    private void Start()
    {
        button = FindObjectOfType<switchChanger>();
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

        if(Physics.Raycast(ray, out hit, reachRange))
        {
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLock>();

            if(codeLock != null)
            {
                //if(button.isPowerOn)
                //{
                //    //string value = hit.transform.name;
                //    //codeLock.SetValue(value);
                //}

                string value = hit.transform.name;
                codeLock.SetValue(value);
            }
        }
    }
}
