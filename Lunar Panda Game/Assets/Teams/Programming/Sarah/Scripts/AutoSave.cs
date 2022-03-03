using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    TestingSave manager;

    private void Start()
    {
        manager = FindObjectOfType<TestingSave>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            manager.save();
            gameObject.SetActive(false);
        }
    }

}
