using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Document Data")]
public class DocumentData : ScriptableObject
{  
    public string id;
    public string documentName;
    public string docText;
    public bool isClue;
    public Image inventoryIcon;
}
