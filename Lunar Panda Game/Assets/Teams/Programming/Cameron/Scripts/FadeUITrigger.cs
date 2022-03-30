using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUITrigger : MonoBehaviour
{
    GameObject player;
    [SerializeField] CanvasGroup fadeItem;
    [SerializeField] float fadeTime = 1f;
    bool faded = false;

    private void Awake()
    {
        player = FindObjectOfType<playerMovement>().gameObject;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject == player && !faded)
        {
            StartCoroutine(FadeIn());
            faded = true;
        }
    }

    IEnumerator FadeIn()
    {
        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeTime;
            fadeItem.alpha = Mathf.Lerp(0, 1, normalizedTime);
            yield return null;
        }
        fadeItem.alpha = 1;
        Time.timeScale = 0;
        yield return null;
    }
}
