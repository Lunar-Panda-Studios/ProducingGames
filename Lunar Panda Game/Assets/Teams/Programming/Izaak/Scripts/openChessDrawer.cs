using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChessDrawer : MonoBehaviour
{
    [Header("Movement Parameters")]
    [Tooltip("Speed the drawer opens. (Don't set this too high)")]
    public float moveSpeed;
    private float currentSpeed;

    [Tooltip("How far the drawer must move to be considered open. Is an x value relative to the chessboard")]
    public float xValueOpened;

    private Rigidbody r_body;

    [Header("Game Objects")]
    [Tooltip("The item (likely clock hand) which is stored inside the drawer")]
    public GameObject chessPrize;

    private BoxCollider boxCol;

    //[HideInInspector]
    public bool puzzleCleared = false;
    private bool drawerOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        r_body = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        currentSpeed = Mathf.Abs(xValueOpened - transform.localPosition.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleCleared)
        {
            if (!drawerOpened)
            {
                openDrawer();
            }
        }
    }

    public void checkForComplete()
    {

    }

    public void openDrawer()
    {
        r_body.velocity = (new Vector3(moveSpeed * currentSpeed, 0, 0));
        chessPrize.transform.localPosition = new Vector3(0.5f, 0.4f, -0.5f);
        if (xValueOpened < transform.localPosition.x)
        {
            drawerOpened = true;
            r_body.velocity = new Vector3(0, 0, 0);
            chessPrize.GetComponent<clockPrizeController>().boxCol.enabled = true;
            chessPrize.GetComponent<clockPrizeController>().r_body.constraints = RigidbodyConstraints.None;
            boxCol.enabled = true;
            r_body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            chessPrize.GetComponent<clockPrizeController>().r_body.velocity = new Vector3(0, 0, 0);
        }
    }
}
