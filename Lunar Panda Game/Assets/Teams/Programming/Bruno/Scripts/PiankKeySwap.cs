using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiankKeySwap : MonoBehaviour
{
    public int id;
    [Header("Prefabs")]
    [Tooltip("Destroyable Version of the gameobject")]
    public GameObject PuzzleKey;

    [Header("Misc")]
    [Tooltip("Hammer's item data")]
    public ItemData PianoKey;
    [Tooltip("Inventory's script")]
    public Inventory inventoryScript;

    InteractRaycasting raycast;
    private void Start()
    {
        raycast = FindObjectOfType<InteractRaycasting>(); // Needed to stop the object rotating from activating the script as well
        GameEvents.current.puzzleCompleted += puzzleComplete;
    }

    void Update()
    {
        // Using left mouse button as the interactable key
        if (Input.GetButtonDown("Interact"))
        {
            destroyObject();
        }
    }

    void destroyObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Currently using Connor's raycast script

        if (raycast.raycastInteract(out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                if (inventoryScript.itemInventory[inventoryScript.selectedItem] == PianoKey)
                {
                    GameEvents.current.onPuzzleComplete(id);
                }
            }
        }
    }

    void puzzleComplete(int id)
    {
        if (id == this.id)
        {
            //It instantiates the real piano key version of the game object in the same position as the empty piano key object and destroys the original object
            Instantiate(PianoKey, transform.position, transform.rotation);
            Destroy(gameObject);

            //if (Analysis.current.consent)
            //{
            //    Analysis.current.resetTimer("");
            //}

            PuzzleData.current.completedEvents[id] = true;
            PuzzleData.current.isCompleted[id - 1] = true;
        }
    }
}
