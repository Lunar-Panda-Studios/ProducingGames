using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(menuName = "Document Data")]
public class DocumentData : ScriptableObject
{

    public string documentName;
    [Tooltip("Any that that is on the document")]
    [TextArea]
    public string docText;
    public Sprite documentImage;
    public bool isLandscape;
    public GameObject prefab;
    internal bool beenPickedUp = false;
    internal int timesChecked = 0;
    public Room roomGottenIn;
}

public enum Room
{
    CRASHEDTRAIN,
    TRAIN,
    HOSPITAL,
    HOTEL,
    CABIN,
    NONE
}
