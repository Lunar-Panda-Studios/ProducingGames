using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSelect : MonoBehaviour
{
    CodeLock codeLock;

    int reachRange = 100;

    Transform player;
    Transform cam;

    public bool needsActiveBtnToWork = true;
    [SerializeField] switchChanger buttonNeeded;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;
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
            codeLock = hit.transform.gameObject.GetComponentInParent<CodeLock>();

            if (codeLock != null)
            {
                if (!needsActiveBtnToWork)
                {
                    string value = hit.transform.name;
                    codeLock.SetValue(value);
                }
                else if (buttonNeeded != null)
                {
                    if (buttonNeeded.isPowerOn)
                    {
                        string value = hit.transform.name;
                        codeLock.SetValue(value);
                    }
                }
            }
        }
    }
}
