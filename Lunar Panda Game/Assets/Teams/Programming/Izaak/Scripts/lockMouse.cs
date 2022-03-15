using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockMouse : MonoBehaviour
{
    [Tooltip("References the player's Transform component")]
    public Transform playerObj;

    [Header("Camera Settings")]
    [Tooltip("Speed that the camera rotates with the mouse")]
    public float cameraRotateSpeed;

    private float mouseX;
    private float mouseY;

    private float xRotation = 0.0f;
    private float newYRotation = 0;

    [Header("Clamp Settings")]
    [Tooltip("The maximum and minimum angle the player can look vertically respectively")]
    public float cameraUpperBoundsX;
    public float cameraLowerBoundsX;
    public float cameraUpperBoundsY;
    public float cameraLowerBoundsY;

    [SerializeField] internal bool canLook;

    void Awake()
    {
        canLook = true;
    }
    
    void Update()
    {
        if (canLook)
        {
            //Locks the cursor to the center of the screen and makes it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //Collects the current movement of the mouse on this frame multiplied by the speed variable
            mouseX = Input.GetAxis("Mouse X") * cameraRotateSpeed;
            mouseY = Input.GetAxis("Mouse Y") * cameraRotateSpeed;

            //Locks the player to only being able to look up and down within the set boundaries
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, cameraLowerBoundsX, cameraUpperBoundsX);

            //Rotates the camera and player by the values collected previously
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerObj.Rotate(Vector3.up * mouseX);
        }
    }
}
