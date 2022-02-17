using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectInputsAndOutputs : MonoBehaviour
{
    public int id;
    Transform player;
    Transform cam; //not referring to me, referring to the camera
    [SerializeField] List<GameObject> InputNodes;
    [HideInInspector] public GameObject inputCurrentlyConnecting;
    [SerializeField] float lineWidth;
    LineRenderer line;
    [Tooltip("The distance between the camera and the cable while the players holding it")]
    [SerializeField] float lineHoldDist;
    [SerializeField] GameObject button;
    [SerializeField] Light completionLight;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;
        completionLight.enabled = false;
    }

    private void Start()
    {
        GameEvents.current.puzzleCompleted += puzzleCompleted;
    }

    void Update()
    {
        CheckForButtonPress();
        //if the switch is set to off
        if (!button.GetComponent<switchChanger>().getSwitchState())
        {
            RaycastHit hit;
            if (Input.GetButtonDown("Interact"))
            {
                if (inputCurrentlyConnecting)
                {
                    //just realised this is irrelevent. I'll fix after prototype is out
                    if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
                    {
                        if (hit.transform.CompareTag("OutputNode"))
                        {
                            //if the raycast hits an output node, lock the line renderer to it and update the input node so it knows
                            //that its connected now
                            inputCurrentlyConnecting.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
                            inputCurrentlyConnecting.GetComponent<Node>().connectedNode = hit.transform.gameObject;
                        }
                        else
                        {
                            Destroy(inputCurrentlyConnecting.GetComponent<LineRenderer>());
                        }

                        inputCurrentlyConnecting = null;
                    }
                }

                if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
                {
                    //loop through all the input nodes, and if the raycast hit one of em, set up the line renderer and stuff
                    foreach (GameObject i in InputNodes)
                    {
                        if (hit.transform.gameObject == i)
                        {
                            inputCurrentlyConnecting = i;
                            inputCurrentlyConnecting.GetComponent<Node>().connectedNode = null;
                            SetUpLine();
                        }
                    }
                    
                }
            }
            if (inputCurrentlyConnecting)
            {
                DrawLine();
            }
        }

    }

    void CheckForButtonPress()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                if (hit.transform.gameObject == button)
                {
                    button.GetComponent<switchChanger>().changeSwitchState();
                    if (CheckCombination())
                    {
                        button.GetComponent<switchChanger>().TurnPowerOn();
                        completionLight.enabled = true;
                        GameEvents.current.onPowerTurnedOn(id);
                        GameEvents.current.onPuzzleComplete(id);
                    }
                    else
                    {
                        completionLight.enabled = false;
                        GameEvents.current.onPowerTurnedOff(id);
                        button.GetComponent<switchChanger>().TurnPowerOff();
                    }
                }
            }
        }
        }

    void SetUpLine()
    {
        //can reuse line renderers if one has already been created (this happens if a node has already been interacted with before)
        if (!inputCurrentlyConnecting.GetComponent<LineRenderer>())
        {
            line = inputCurrentlyConnecting.AddComponent<LineRenderer>();
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = 2;
            line.material = inputCurrentlyConnecting.GetComponent<Node>().lineMat;
            line.useWorldSpace = true;
        }
    }

    void DrawLine()
    {
        //doing this if statement each frame while connecting thingies sucks, but to fix it id need at least a small brain
        if (inputCurrentlyConnecting.GetComponent<LineRenderer>())
        {
            //render the line from the input node, to where the player is looking at
            inputCurrentlyConnecting.GetComponent<LineRenderer>().SetPosition(0, inputCurrentlyConnecting.transform.position);
            inputCurrentlyConnecting.GetComponent<LineRenderer>().SetPosition(1, cam.position + (cam.forward * lineHoldDist));
        }
    }

    public bool CheckCombination()
    {
        //loop through all the input nodes
        foreach (GameObject i in InputNodes)
        {
            //if the currently connected node is not the right node, return false
            if (i.GetComponent<Node>().connectedNode != i.GetComponent<Node>().desiredNode)
            {
                return false;
            }
        }
        //if all the nodes are connected to the right nodes, return true
        return true;
    }

    public void puzzleCompleted(int id)
    {
        if(id == this.id)
        {
            if (!CheckCombination())
            {
                for (int i = 0; i < InputNodes.Count; i++)
                {
                    if(InputNodes[i].GetComponent<LineRenderer>() == null)
                    {
                        line = InputNodes[i].AddComponent<LineRenderer>();
                        line.startWidth = lineWidth;
                        line.endWidth = lineWidth;
                        line.positionCount = 2;
                        line.material = InputNodes[i].GetComponent<Node>().lineMat;
                        line.useWorldSpace = true;
                    }
                    line.SetPosition(0, InputNodes[i].GetComponent<Node>().desiredNode.transform.position);
                    line.SetPosition(1, InputNodes[i].GetComponent<Node>().transform.position);
                    line.GetComponent<Node>().connectedNode = InputNodes[i].GetComponent<Node>().desiredNode.transform.gameObject;
                }

                print("Auto Completed");
            }

            PuzzleData.current.completedEvents[id] = true;
            PuzzleData.current.isCompleted[id - 1] = true;
        }
    }
}
