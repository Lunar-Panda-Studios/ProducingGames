using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.autoSavingPromptShow();
            GameManager.Instance.save(true);
            gameObject.SetActive(false);

        }
    }

}
