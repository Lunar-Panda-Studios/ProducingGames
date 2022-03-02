using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCamera : MonoBehaviour
{
    private Camera MCamera;

    private void Awake()
    {
        MCamera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(MCamera.transform.position, Vector3.up);
    }
}
