using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChessDrawer : MonoBehaviour
{
    public float moveSpeed;
    private float currentSpeed;

    public float xValueOpened;

    public Rigidbody r_body;

    [HideInInspector]
    public bool puzzleCleared = false;
    private bool drawerOpened = false;
    // Start is called before the first frame update
    void Start()
    {
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
        if (xValueOpened < transform.localPosition.x)
        {
            drawerOpened = true;
            r_body.velocity = new Vector3(0, 0, 0);
        }
    }
}
