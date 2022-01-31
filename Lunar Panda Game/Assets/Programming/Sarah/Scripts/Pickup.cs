using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ItemData data;
    Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void OnMouseDown()
    {
        inventory.addItem(data);
        Destroy(gameObject);
    }
}
