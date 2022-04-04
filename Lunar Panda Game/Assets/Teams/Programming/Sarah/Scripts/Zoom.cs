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
    PlayerCrouch playerCrouch;
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
        playerCrouch = FindObjectOfType<PlayerCrouch>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delay)
        {
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
                    timer = 0;
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
                timer = 0;
                canZoom = false;
                playerCrouch.enabled = false;
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
                playerMesh.SetActive(true);
                mouse.canLook = true;
                player.enabled = true;
                isZoomed = false;
                zoomCollider.enabled = true;
                flashlight.GetComponent<HDAdditionalLightData>().intensity = 27500;
                zoomOut = false;
                timer = 0;
                canZoom = false;
                playerCrouch.enabled = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            unZoom();
        }
    }

    public void unZoom()
    {
        zoomOut = true;
    }

    public bool canDisable()
    {
        if (mainCam.transform.position == camPositionOG.position)
        {
            return true;
        }

        return false;
    }
}
