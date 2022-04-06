using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorNoRotation : MonoBehaviour
{
    public string audioClip;
    public ItemData key;
    Inventory inventory;
    [SerializeField] InteractAnimation interactAnimation;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycasting.Instance.raycastInteract(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (key == inventory.itemInventory[inventory.selectedItem])
                    {
                        Unlock();
                    }
                }
            }
        }
    }

    public void Unlock()
    {
        SoundEffectManager.GlobalSFXManager.PlaySFX(audioClip);
        interactAnimation.enabled = true;
        this.enabled = false;
    }
}