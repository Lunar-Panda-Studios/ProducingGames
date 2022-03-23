using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySelect : MonoBehaviour
{
    PianoSequenceCheck sequence;

    //int reachRange = 100;

    Transform player;
    Transform cam;    

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
            sequence = hit.transform.gameObject.GetComponentInParent<PianoSequenceCheck>();

            if (sequence != null && hit.transform.CompareTag("SequenceCode"))
            {
                string value = hit.transform.name;
                sequence.SetValue(value);
            }
        }
    }
}
