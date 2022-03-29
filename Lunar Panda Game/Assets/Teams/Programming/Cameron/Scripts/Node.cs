using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Tooltip("The material of the wire that comes out of this node. The shader has to be HDRP/Unlit for this")]
    public Material lineMat;
    [Tooltip("The output node that needs to be connected to this one")]
    public GameObject desiredNode;
    [HideInInspector] public GameObject connectedNode;

    //i'll find a better use for this script i swear

    //where is it?

}
