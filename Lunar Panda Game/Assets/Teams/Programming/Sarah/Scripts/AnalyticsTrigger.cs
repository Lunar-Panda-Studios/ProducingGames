using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsTrigger : MonoBehaviour
{
    public string key;

    private void OnTriggerEnter(Collider other)
    {
        if (Analysis.current != null)
        {
            if (Analysis.current.consent && (!Analysis.current.timersPuzzlesp2.ContainsKey(key) && !Analysis.current.timersPuzzlesp1.ContainsKey(key)))
            {
                Analysis.current.resetTimer(key);
            }

            gameObject.SetActive(false);
        }
    }
}
