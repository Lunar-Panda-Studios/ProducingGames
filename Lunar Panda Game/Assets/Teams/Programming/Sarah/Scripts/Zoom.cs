using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    Camera mainCam;
    public Transform moveToLocation;
    public float disDelta;
    public GameObject playerMesh;
    internal bool zoomIn = false;
    internal bool isZoomed = false;
    playerMovement player;
    lockMouse mouse;
    InteractRaycasting playerPickupRay;
    public Transform camPositionOG;
    bool zoomOut = false;

    private void Start()
    {
        mainCam = Camera.main;
        player = FindObjectOfType<playerMovement>();
        mouse = FindObjectOfType<lockMouse>();
        playerPickupRay = FindObjectOfType<InteractRaycasting>();
    }

    private void Update()
    {
        if (zoomIn)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, moveToLocation.position, disDelta);

            if(mainCam.transform.position == moveToLocation.position)
            {
                zoomIn = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isZoomed = true;
            }

            mainCam.transform.LookAt(gameObject.transform);
        }

        if(zoomOut)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, camPositionOG.position, disDelta);

            if (mainCam.transform.position == camPositionOG.position)
            {
                zoomOut = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerMesh.SetActive(false);
                mouse.canLook = true;
                player.enabled = true;
                isZoomed = false;
            }
        }

        RaycastHit hit;
        if (Input.GetButton("Interact"))
        {
            if (playerPickupRay.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject && !isZoomed)
                {
                    mouse.canLook = false;
                    player.enabled = false;
                    player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    playerMesh.SetActive(false);
                    zoomIn = true;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            unZoom();
        }
    }

    public void unZoom()
    {
        zoomOut = true;
    }
}
