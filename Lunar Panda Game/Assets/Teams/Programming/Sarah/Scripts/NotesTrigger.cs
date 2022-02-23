using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTrigger : MonoBehaviour
{
    internal int id;
    public StoryData notesData;
    Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        GameEvents.current.triggerStoryNotes += obtained;
        id = notesData.id;
    }

    public void obtained(int id)
    {
        if(id == this.id)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inventory.addItem(notesData);
            GameEvents.current.onTriggerStoryNotes(id);
        }
    }
}
