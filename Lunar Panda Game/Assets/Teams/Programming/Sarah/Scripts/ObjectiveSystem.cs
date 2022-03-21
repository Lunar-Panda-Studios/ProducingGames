using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSystem : MonoBehaviour
{
    internal Objectives currentObjective;

    private void Start()
    {
        currentObjective = Database.current.objectives[0];
    }

    public void onCompletion()
    {
        if(currentObjective != Database.current.objectives[Database.current.objectives.Count - 1])
        {
            currentObjective = Database.current.objectives[currentObjective.id + 1];
            UIManager.Instance.updateObject();
        }
    }
}
