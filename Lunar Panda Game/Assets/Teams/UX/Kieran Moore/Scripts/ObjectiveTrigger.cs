using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectiveTrigger : MonoBehaviour
{
    ObjectiveSystem objectives;

    private void Start()
    {
        objectives = FindObjectOfType<ObjectiveSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            objectives.onCompletion();
        }

    }
}