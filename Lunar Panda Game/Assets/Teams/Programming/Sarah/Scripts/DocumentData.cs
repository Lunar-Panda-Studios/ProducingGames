using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Document Data")]
public class DocumentData : ScriptableObject
{  
    public string documentName;
    [Tooltip("Any that that is on the document")]
    [TextArea]
    public string docText;
    public Sprite documentImage;
    public GameObject prefab;
    public Room roomGottenIn;
}

public enum Room
{
    TRAIN,
    HOSPITAL,
    HOTEL,
    CABIN,
    MANSION,
    CATHEDRAL
}
