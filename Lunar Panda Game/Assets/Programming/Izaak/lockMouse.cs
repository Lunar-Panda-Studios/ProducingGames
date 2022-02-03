using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockMouse : MonoBehaviour
{
    public float cameraRotateSpeed;

    public Transform playerObj;

    private float mouseX;
    private float mouseY;

    private float xRotation = 0.0f;
    private float newYRotation = 0;

    public float cameraUpperBoundsX;
    public float cameraLowerBoundsX;
    public float cameraUpperBoundsY;
    public float cameraLowerBoundsY;

    public bool canLook;
    
    void Start()
    {
        canLook = true;
    }

    void Update()
    {
        if (canLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseX = Input.GetAxis("Mouse X") * cameraRotateSpeed;
            mouseY = Input.GetAxis("Mouse Y") * cameraRotateSpeed;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, cameraLowerBoundsX, cameraUpperBoundsX);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerObj.Rotate(Vector3.up * mouseX);
        }
    }
 }
