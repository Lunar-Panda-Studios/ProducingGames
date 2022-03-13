using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchTrigger : MonoBehaviour
{
    public bool isObjectAbove = false;
    private void OnTriggerStay(Collider other)
    {
        isObjectAbove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isObjectAbove = false;
    }
}
