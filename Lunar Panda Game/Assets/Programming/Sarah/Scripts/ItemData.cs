using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Data")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    internal int id;
    public ItemData database;
    public string itemName;
    public Sprite itemImage;
    public GameObject prefab;
}
