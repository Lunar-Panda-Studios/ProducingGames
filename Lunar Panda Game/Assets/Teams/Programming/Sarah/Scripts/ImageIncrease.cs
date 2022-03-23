using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageIncrease : MonoBehaviour
{
    public Image cutscene;
    public float maxTimer = 1;
    public Vector2 scaleTo;
    public float second = 0.2f;

    // Update is called once per frame
    void Update()
    {
        cutscene.rectTransform.localScale = Vector2.Lerp(cutscene.rectTransform.localScale, scaleTo, Time.deltaTime * second);
    }
}
