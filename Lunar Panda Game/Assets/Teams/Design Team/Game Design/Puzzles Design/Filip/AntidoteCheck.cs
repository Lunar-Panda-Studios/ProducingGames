using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteCheck : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] UnlockDoorNoRotation unlock;
    [SerializeField] ItemData itemData;

    bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.itemInventory.Contains(itemData))
        {
            if (doOnce)
            {
                doOnce = false;
                unlock.Unlock();
            }
        }
    }
}
