using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    public bool shouldSave = false;
    public bool mainSave = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && shouldSave)
        {
            UIManager.Instance.autoSavingPromptShow();
            GameManager.Instance.save(mainSave);
            gameObject.SetActive(false);

        }
    }

}
