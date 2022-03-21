using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective")]
[System.Serializable]
public class Objectives : ScriptableObject
{
    internal int id;
    public string description;
    internal bool isCompleted;
}
