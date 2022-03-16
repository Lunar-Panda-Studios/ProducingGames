using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    private Rigidbody r_body;

    [Header("Game Objects")]
    [Tooltip("Reference to parent object. Drag it in if it's not there already")]
    public GameObject puzzleParent;
    private pressurePlatePuzzle puzzleScript;

    [Header("Plate Values")]
    [Tooltip("This number is a stand-in for a symbol. Use numbers from 0-5")]
    public int symbolNo;

    [Tooltip("How fast the pressure plate rises and falls")]
    public float moveSpeed;
    [Tooltip("How high the object goes when unpressed")]
    public float maxYVal;
    [Tooltip("How low the object goes when pressed")]
    public float minYVal;
    [Tooltip("Whether the object is pressed or not")]
    public bool pressing;
    [HideInInspector]
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        r_body = GetComponent<Rigidbody>();
        puzzleScript = puzzleParent.GetComponent<pressurePlatePuzzle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (pressing)
            {
                if (transform.position.y > minYVal)
                {
                    r_body.velocity = -transform.up * moveSpeed * Time.deltaTime;
                }
                else
                {
                    moving = false;
                    r_body.velocity = new Vector3(0,0,0);
                    puzzleScript.checkIfCorrect(symbolNo);
                }
            }
            else
            {
                if (transform.position.y < maxYVal)
                {
                    r_body.velocity = transform.up * moveSpeed * Time.deltaTime;
                }
                else
                {
                    moving = false;
                    r_body.velocity = new Vector3(0, 0, 0);
                }
            }
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == ("Player"))
        {
            moving = true;
            pressing = true;
        }
    }
}
