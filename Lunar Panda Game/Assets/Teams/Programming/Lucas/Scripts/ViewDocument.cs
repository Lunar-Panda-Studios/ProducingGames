using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewDocument : MonoBehaviour
{
    internal bool showText = false;
    internal bool showDoc = false;
    internal bool inInventory = false;
    [Tooltip("Enter the corrisponding data (scripable object) for this document")]
    public DocumentData data;

    [Header("Inputs")]
    //public KeyCode keyInteract; //temp
    public KeyCode keyText; //temp
    private bool overDoc; //temp
    private Inventory inventory;
    internal playerMovement player;
    internal lockMouse lockMouse;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<playerMovement>();
        lockMouse = FindObjectOfType<lockMouse>();

    }

    //temp testing
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && !showDoc)
        {
            RaycastHit hit;
            if(InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if(hit.transform.gameObject == gameObject)
                {
                    UIManager.Instance.showDocument(data, this);
                    inInventory = true;
                    player.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    player.enabled = false;
                    lockMouse.canLook = false;
                }
            }
        }
        else if (Input.GetButtonDown("Interact") && showDoc)
        {
            UIManager.Instance.hideDocument(this);
            UIManager.Instance.hideText(this);

            player.enabled = true;
            lockMouse.canLook = true;
        }

        if (Input.GetButtonDown("ShowText") && showDoc)
        {
            if(showText)
            {
                UIManager.Instance.hideText(this);
            }
            else
            {
                UIManager.Instance.showingText(data, this);
            }
        }
    }
}
