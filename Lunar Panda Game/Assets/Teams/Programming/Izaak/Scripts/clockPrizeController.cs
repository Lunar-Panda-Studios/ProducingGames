using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockPrizeController : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("Drag the drawer in the scene here")]
    public GameObject drawerParent;

    [HideInInspector]
    public BoxCollider boxCol;
    [HideInInspector]
    public Rigidbody r_body;


    // Start is called before the first frame update
    void Start()
    {
        r_body = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;
        transform.parent = drawerParent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(r_body.velocity);
    }
}
