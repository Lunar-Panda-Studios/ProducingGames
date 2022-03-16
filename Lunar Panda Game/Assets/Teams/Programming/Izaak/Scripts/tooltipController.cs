using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltipController : MonoBehaviour
{
    public enum controlTypes
    {
        MOVE, JUMP, SPRINT, CROUCH, FLASHLIGHT
    }
    [Header("Controls")]
    [Tooltip("The action you must complete to get rid of the tooltip")]
    public controlTypes input;

    [Header("Parameters")]
    [Tooltip("The message that pops up in the tooltip")]
    public string tooltipMessage;

    [Tooltip("The range the player must be in to activate the tooltip (set to the same as the sphere collider radius)")]
    public float tooltipRange;
    private bool inRange = false;

    [Header("Game Objects")]
    [Tooltip("Drag the tooltip text in the scene here")]
    public GameObject UITip;

    // Update is called once per frame
    void Update()
    {
        checkControls();
    }

    void checkControls()
    {
        if (inRange)
        {
            Debug.Log(inRange);
            switch (input)
            {
                case controlTypes.MOVE:
                    {
                        if ((Input.GetButton("Horizontal")) || (Input.GetButton("Vertical")))
                        {
                            deactivateTooltip();
                        }
                    }
                    break;
                case controlTypes.JUMP:
                    {
                        if (Input.GetButton("Jump"))
                        {
                            deactivateTooltip();
                        }
                    }
                    break;
                case controlTypes.CROUCH:
                    {
                        if (Input.GetButton("Crouch"))
                        {
                            deactivateTooltip();
                        }
                    }
                    break;
                case controlTypes.FLASHLIGHT:
                    {
                        if (Input.GetButton("Flashlight"))
                        {
                            deactivateTooltip();
                        }
                    }
                    break;
                case controlTypes.SPRINT:
                    {
                        if (Input.GetButton("Sprint"))
                        {
                            deactivateTooltip();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void deactivateTooltip()
    {
        inRange = false;
        UITip.GetComponent<tooltipDisplay>().changeText(" ");
        UITip.SetActive(false);
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            inRange = true;
            UITip.SetActive(true);
            UITip.GetComponent<tooltipDisplay>().changeText(tooltipMessage);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            inRange = false;
            UITip.GetComponent<tooltipDisplay>().changeText(" ");
            UITip.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, tooltipRange);
    }
}
