using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int id;
    [Header("Prefabs")]
    [Tooltip("Destroyable Version of the gameobject")]
    public GameObject destroyedVersion;

    [Header("Misc")]
    [Tooltip("Hammer's item data")]
    public ItemData Hammer;
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
                if (inventoryScript.itemInventory[inventoryScript.selectedItem] == Hammer)
                {
                    GameEvents.current.onPuzzleComplete(id);
                }
            }            
        }
    }

    void puzzleComplete(int id)
    {
        if(id == this.id && this.gameObject != null)
        {
            
            //It instantiates the destroyable version of the game object in the same position as the original object and destroys the original object
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);

            if (Analysis.current.consent)
            {
                Analysis.current.resetTimer("Destructable Object");
            }

            PuzzleData.current.completedEvents[id] = true;
            PuzzleData.current.isCompleted[id - 1] = true;
        }
    }
    
}
