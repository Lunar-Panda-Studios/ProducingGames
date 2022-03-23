using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class BoxToDestroy : MonoBehaviour
{
    [SerializeField] GameObject destroyObject;
    [SerializeField] GameObject unhideObject;
    [SerializeField] ItemData requiredItem;

    public VideoPlayer videoPlayer;
    public VideoClip videoClip;

    bool played = false;

    Inventory inv;

    private void Awake()
    {
        inv = FindObjectOfType<Inventory>();
        
    }

    private void Start()
    {
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.Prepare();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if(inv.itemInventory[inv.selectedItem] == requiredItem || requiredItem == null)
            {
                if (InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        if(destroyObject != null)
                            destroyObject.SetActive(false);
                        videoPlayer.Play();
                        played = true;
                        unhideObject.SetActive(true);
                    }
                }
            }
        }

        if (!videoPlayer.isPlaying && played)
        {
            videoPlayer.clip = null;
        }
    }   
}
