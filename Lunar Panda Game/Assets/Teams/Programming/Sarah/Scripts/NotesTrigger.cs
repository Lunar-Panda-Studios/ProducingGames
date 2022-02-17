using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTrigger : MonoBehaviour
{
    public StoryData notesData;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Add notes to story inventory
            gameObject.SetActive(false);
        }
    }
}
