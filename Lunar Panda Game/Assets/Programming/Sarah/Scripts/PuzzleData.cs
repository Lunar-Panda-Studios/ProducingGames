using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleData: MonoBehaviour
{
    public static PuzzleData current;
    [Tooltip("ID of each puzzle element in order of completion and if it is completed")]
    public List<int> eventsID;
    public List<bool> isCompleted;
    public Dictionary<int,bool> completedEvents = new Dictionary<int, bool>();

    private void Start()
    {
        createDictionary();
        current = this;
    }

    public void createDictionary()
    {
        completedEvents = new Dictionary<int, bool>();
        for (int i = 0; i < eventsID.Count; i++)
        {
            completedEvents.Add(eventsID[i], isCompleted[i]);
            if(isCompleted[i])
            {
                GameEvents.current.onPuzzleComplete(i);
            }
        }
    }
}
