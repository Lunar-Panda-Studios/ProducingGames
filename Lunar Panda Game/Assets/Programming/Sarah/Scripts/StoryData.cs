using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Story Data")]
public class StoryData : ScriptableObject
{
    internal int id;
    public string storyDataName;
    public string description;
}
