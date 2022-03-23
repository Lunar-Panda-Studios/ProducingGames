using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPerformance : MonoBehaviour
{
    //THIS SCRIPT CAN CRASH UNITY AND/OR YOUR PC BE CAREFUL USING IT
    //DO NOT ASSIGN THE SAME KEYCODE TO MORE THAN ONE VARIABLE. DO NOT SPAM ASSIGNED KEYS.
    //GOOD LUCK.

    public int cubeNegative; //will be the negative/lower bound axis of a cube
    public int cubePositive; //will be the positive/upper bound axis of a cube

    public int groundHeight; //enter this for object to not spawn below platform
    public int cubeHeight; //enter this to get max spawn height

    public GameObject spawnThis;

    public KeyCode killSomeFrames; // Assign spawning keys in the inspector
    public KeyCode killMoreFrames;
    public KeyCode killMostFrames;

    void Update()
    {
        if (Input.GetKeyDown(killSomeFrames)) //spawns 1 of the game object assigned in the inspector
        {
            KillYourPC();
        }

        if (Input.GetKeyDown(killMoreFrames)) //spawns 10 of the game object assigned in the inspector
        {
            for (int i = 0; i < 10; i++)
            {
                KillYourPC();
            }
        }

        if (Input.GetKeyDown(killMostFrames)) //spawns 100 of the game object assigned in the inspector
        {
            for (int i = 0; i < 100; i++)
            {
                KillYourPC();
            }
        }
    }

    void KillYourPC()
    {
        Vector3 pos = new Vector3(Random.Range(cubeNegative, cubePositive), Random.Range(groundHeight, cubeHeight), Random.Range(cubeNegative, cubePositive));
        GameObject.Instantiate(spawnThis).transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, 0)); //spawns game object at random range
    }

}
