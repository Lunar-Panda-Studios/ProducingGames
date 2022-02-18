using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Story Data")]
public class StoryData : ScriptableObject
{
    public string storyDataName;
    [TextArea]
    public string description;
    public Room roomGottenIn;
}
