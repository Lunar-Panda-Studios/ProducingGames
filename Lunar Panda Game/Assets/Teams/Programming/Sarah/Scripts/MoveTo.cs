using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    internal bool move;
    Vector3 startLocation;
    public float speed = 3;
    public GameObject moveTo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (transform.position != moveTo.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveTo.transform.position, speed * Time.deltaTime);

                if (moveTo.transform.position == transform.position)
                {
                    move = false;
                }
            }

        }
    }
}
