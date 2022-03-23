using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearSeeSayMaster : MonoBehaviour
{
    ItemCanBePlaced CovidBear;
    ItemCanBePlaced DeafBear;
    ItemCanBePlaced MuteBear;

    public bool isCompleted;

    public GameObject door;

    public void check()
    {
        if (CovidBear.isItemPlaced && DeafBear.isItemPlaced && MuteBear.isItemPlaced)
        {
            isCompleted = true;
            door.SetActive(false);
        }
    }
}
