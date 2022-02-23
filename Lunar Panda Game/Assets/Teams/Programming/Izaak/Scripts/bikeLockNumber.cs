using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikeLockNumber : MonoBehaviour
{
    [Header("Digit Values")]
    [Tooltip("Where this number places in the sequence")]
    public int digitPlacement;

    private float rotationIncrement = 36;
    private int currentNumber = 5;
    private GameObject bikeLockParent;
    private bikeLock bikeLockScript;

    private GameObject player;
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        //References the parent object and its script
        bikeLockParent = transform.parent.gameObject;
        bikeLockScript = bikeLockParent.GetComponent<bikeLock>();
        //Matches the current value and rotation with the current code value in the parent script
        currentNumber = bikeLockScript.getCurrentCode(digitPlacement);
        transform.Rotate((-5 * rotationIncrement) + (currentNumber * rotationIncrement), 0, 0);
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                if (hit.transform.gameObject == gameObject)
                {
                        if (currentNumber >= 9)
                        {
                            currentNumber = 0;
                        }
                        else
                        {
                            currentNumber++;
                        }
                        transform.Rotate(-rotationIncrement, 0, 0);
                        bikeLockScript.changeCurrentCode(digitPlacement, currentNumber);
                        bikeLockScript.checkPuzzleComplete();
                }
            }
        }
    }
}
