using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToDestroy : MonoBehaviour
{
    [SerializeField] GameObject destroyObject;
    [SerializeField] ItemData requiredItem;
    Inventory inv;

    private void Awake()
    {
        inv = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if(inv.itemInventory[inv.selectedItem] == requiredItem || requiredItem == null)
            {
                if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        destroyObject.SetActive(false);
                    }
                }
            }
        }
    }
}
