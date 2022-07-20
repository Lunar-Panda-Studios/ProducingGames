using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    //Author: Matej Gajdos - Game Design
    //Original script was Bruno's 'Temp Select'
    //I simplified the code to work more effectively with the partner script 'SimplifiedCodeLock'

    public SimplifiedCodeLock codeLock;
    private void Start()
    {
        codeLock = FindObjectOfType<SimplifiedCodeLock>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            CheckHitObj();
        }
    }
    void CheckHitObj()
    {
        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                Debug.Log("Button got hit");
                string value = hit.transform.name;
                codeLock.SetValue(value);
            }
        }
    }
}
