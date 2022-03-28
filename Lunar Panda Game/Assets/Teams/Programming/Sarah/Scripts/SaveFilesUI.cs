using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFilesUI : MonoBehaviour
{
    public List<GameObject> slots;
    public List<Image> slotImages;
    public List<Sprite> levelImages;

    // Start is called before the first frame update
    void Start()
    {
        int level;

        for (int i = 0; i < 3; i++)
        {
            if (SaveSystem.pathExists(i))
            {
                level = SaveSystem.checkLevel();
                slots[i].SetActive(true);

                slotImages[i].sprite = levelImages[level];
            }
            else
            {
                slots[i].SetActive(false);
                slotImages[i].sprite = levelImages[0];
            }
        }
    }
}
