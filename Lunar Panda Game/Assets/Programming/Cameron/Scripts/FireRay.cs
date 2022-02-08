using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRay : MonoBehaviour
{
    public GameObject Fire(Transform fireFromTransform, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(fireFromTransform.position, fireFromTransform.TransformDirection(Vector3.forward), out hit, distance))
            return hit.transform.gameObject;
        else
            return null;
    }

    public GameObject Fire(Vector3 fireFromPosition, Vector3 fireWithRotation, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(fireFromPosition, fireWithRotation, out hit, distance))
            return hit.transform.gameObject;
        else
            return null;
    }
}
