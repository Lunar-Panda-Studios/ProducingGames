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

    public float cameraUpperBoundsX;
    public float cameraLowerBoundsX;
    public float cameraUpperBoundsY;
    public float cameraLowerBoundsY;
    
    void Start()
    {
        
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mouseX = Input.GetAxis("Mouse X") * cameraRotateSpeed;
        mouseY = Input.GetAxis("Mouse Y") * cameraRotateSpeed;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, cameraLowerBoundsX, cameraUpperBoundsX);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerObj.Rotate(Vector3.up * mouseX);

        //if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        //{
        //    transform.eulerAngles += new Vector3(cameraRotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime, -cameraRotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0);
        //}
        //checkBounds();
    }

    private void checkBounds()
    {
        if (transform.eulerAngles.x > cameraUpperBoundsX)
        {
            transform.eulerAngles = new Vector3(cameraUpperBoundsX, transform.eulerAngles.y, 0);
        }
        if (transform.eulerAngles.x < cameraLowerBoundsX)
        {
            transform.eulerAngles = new Vector3(cameraLowerBoundsX, transform.eulerAngles.y, 0);
        }
    }
}
