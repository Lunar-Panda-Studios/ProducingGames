using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSafe : MonoBehaviour
{
    [Header("Safe Parameters")]
    [Tooltip("Speed the safe opens")]
    public float openSpeed;
    [Tooltip("How far the safe opens before the door stops moving")]
    public float maxOpenAngle;

    private bool isOpening = false;
    private bool isChanging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        openCloseSafe();
    }

    public void openCloseSafe()
    {
        if (isChanging)
        {
            if (isOpening)
            {
                transform.Rotate(0, -openSpeed * Time.deltaTime, 0);
                if (transform.eulerAngles.y <= (360 - maxOpenAngle))
                {
                    isChanging = false;
                }
            }
            else
            {
                transform.Rotate(0, openSpeed * Time.deltaTime, 0);
                if (transform.eulerAngles.y >= 359)
                {
                    isChanging = false;
                }
            }
        }
    }

    public void toggleOpening(bool truth)
    {
        isOpening = truth;
        isChanging = true;
    }
}
