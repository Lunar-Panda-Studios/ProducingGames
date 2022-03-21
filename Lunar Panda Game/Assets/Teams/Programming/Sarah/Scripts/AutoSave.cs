using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    GameManager manager;
    public bool shouldSave = false;
    public bool mainSave = false;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && shouldSave)
        {
            manager.save(mainSave);
            gameObject.SetActive(false);
        }
    }

}
