using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCanBePlaced : MonoBehaviour
{
    [SerializeField] ItemData placeableItem;

    //IMPORTANT FOR DESIGNERS IF UR SNOOPING: read the below tooltip before changing anything. It may lead to you being able to fix the issue you have

    [Tooltip("This should be the local position of where the placeable item should be placed. If you also need the rotation and scale changed, change that in the prefab of the object itself")]
    [SerializeField] Vector3 placeableItemPosition;

    Inventory inventory;
    public string clipName;//Matej changes

    internal bool isItemPlaced = false;

    HearSeeSayController controller;

    void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        controller = FindObjectOfType<HearSeeSayController>();
    }

    void Update()
    {
        if (!Input.GetButtonDown("Interact")) return; //hate having such a long if chain so i did this. Dont care. If ur changing this script look at this line.
        if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                if (inventory.itemInventory[inventory.selectedItem] == placeableItem) //if the selected item is the one needed
                {
                    //place item
                    SoundEffectManager.GlobalSFXManager.PlaySFX(clipName);//Matej changes
                    GameObject placedItem = Instantiate(placeableItem.prefab);
                    Destroy(placedItem.GetComponent<Rigidbody>());
                    Destroy(placedItem.GetComponent<GlowWhenLookedAt>());
                    Destroy(placedItem.GetComponent<HoldableItem>());
                    //set this gameobject to be the placed items parent
                    placedItem.transform.parent = transform;
                    placedItem.transform.localPosition = placeableItemPosition;
                    isItemPlaced = true;
                    //remove item from inventory
                    inventory.removeItem();
                    controller.CheckIfComplete();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.TransformPoint(placeableItemPosition), 0.1f);
    }
}