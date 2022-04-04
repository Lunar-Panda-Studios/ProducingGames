using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

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

    [SerializeField] BoxCollider zoomCollider;
    [SerializeField] GameObject flashlight;
    [SerializeField] float flashlightIntensity;

    float timer = 0;
    public float delay = 1.5f;
    bool canZoom;

    private void Start()
    {
        mainCam = Camera.main;
        player = FindObjectOfType<playerMovement>();
        mouse = FindObjectOfType<lockMouse>();
        playerPickupRay = FindObjectOfType<InteractRaycasting>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delay)
        {
            timer = 0;
            canZoom = true;
        }

        RaycastHit hit;
        if (Input.GetButton("Interact"))
        {
            if (playerPickupRay.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject && (!isZoomed || !zoomOut) && canZoom)
                {
                    print("Hit");
                    mouse.canLook = false;
                    player.enabled = false;
                    player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    playerMesh.SetActive(false);
                    zoomIn = true;
                    canZoom = false;
                }
            }
        }

        if (zoomIn)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, moveToLocation.position, disDelta);

            if (mainCam.transform.position == moveToLocation.position)
            {
                zoomIn = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isZoomed = true;
                zoomCollider.enabled = false;
                flashlight.GetComponent<HDAdditionalLightData>().intensity = flashlightIntensity;
                canZoom = false;
            }

            mainCam.transform.LookAt(gameObject.transform);
        }

        if(zoomOut)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, camPositionOG.position, disDelta);

            if (mainCam.transform.position == camPositionOG.position)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerMesh.SetActive(false);
                mouse.canLook = true;
                player.enabled = true;
                isZoomed = false;
                zoomCollider.enabled = true;
                flashlight.GetComponent<HDAdditionalLightData>().intensity = 27500;
                zoomOut = false;
                canZoom = false;
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
