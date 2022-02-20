using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Story Data")]
public class StoryData : ScriptableObject
{
    public int id;
    public string storyDataName;
    [TextArea]
    public string description;
    public Room roomGottenIn;
}
