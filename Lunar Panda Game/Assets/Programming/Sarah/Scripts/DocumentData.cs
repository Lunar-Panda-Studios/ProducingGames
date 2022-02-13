using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Document Data")]
public class DocumentData : ScriptableObject
{  
    public string id;
    public string documentName;
    [Tooltip("Any that that is on the document")]
    public string docText;
    public bool isClue;
    public Sprite inventoryIcon;
    public GameObject prefab;
}
