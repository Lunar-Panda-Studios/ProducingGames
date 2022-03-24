using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HearSeeSayController : MonoBehaviour
{

    [SerializeField] ItemCanBePlaced[] heads = new ItemCanBePlaced[3];
    [SerializeField] bool loadMenuOnComplete = true;
    [SerializeField] string mainMenuName;

    public void CheckIfComplete()
    {
        for (int i = 0; i < heads.Length; i++)
        {
            if (!heads[i].isItemPlaced) return;
        }
        OnComplete();
    }

    void OnComplete()
    {
        if (Analysis.current.consent)
        {
            Analysis.current.resetTimer("HearSeeSay");
            Analysis.current.sendFinal();
        }
        StartCoroutine(LevelManager.Instance.FadeLoadingScreen(mainMenuName));
    }
}
