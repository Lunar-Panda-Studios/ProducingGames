using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ToolTip Type")]
[System.Serializable]
public class ToolTipType : ScriptableObject
{
    public string text;
    public string buttonName;
    public Sprite GamePadSprite;
    public Sprite KeyboardSprite;
}
