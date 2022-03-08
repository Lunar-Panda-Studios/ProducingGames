using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Analysis : MonoBehaviour
{
    internal static Analysis current;
    internal bool consent = false;
    private const string Name = "Game Analystics";
    internal Dictionary<string, object> parameters = new Dictionary<string, object>();

    float timer = 0;
    float levelTimer = 0;

    private void Start()
    {
        if(current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        levelTimer += Time.deltaTime;
    }

    public bool completed()
    {
        AnalyticsResult result = AnalyticsEvent.Custom(Name, parameters);
        print(result.ToString());
        if (result == AnalyticsResult.Ok)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resetTimer(string puzzleName)
    {
        parameters.Add(puzzleName, timer);
        print(parameters[puzzleName]);
        timer = 0;
    }

    public void resetlevelTimer()
    {
        parameters.Add("Time to Complete Level", levelTimer);
        print(parameters["Time to Complete Level"]);
        levelTimer = 0;
    }
}
