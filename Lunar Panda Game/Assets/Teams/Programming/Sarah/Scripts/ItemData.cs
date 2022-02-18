using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Data")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    internal int id;
    public string itemName;
    [TextArea]
    public string description;
    public Sprite itemImage;
    public GameObject prefab;
}
