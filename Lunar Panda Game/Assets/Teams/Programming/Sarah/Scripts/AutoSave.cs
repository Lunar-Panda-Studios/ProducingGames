using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    GameManager manager;
    internal bool shouldSave = false;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && shouldSave)
        {
            manager.save();
            gameObject.SetActive(false);
        }
    }

}
