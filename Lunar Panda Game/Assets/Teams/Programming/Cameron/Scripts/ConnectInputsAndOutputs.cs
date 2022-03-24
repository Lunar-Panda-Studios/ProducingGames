using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectInputsAndOutputs : MonoBehaviour
{
    public int id;
    public bool isSinglePuzzle = true;
    Transform player;
    Transform cam; //not referring to me, referring to the camera
    [SerializeField] List<GameObject> InputNodes;
    [SerializeField] public GameObject inputCurrentlyConnecting;
    [SerializeField] float lineWidth;
    LineRenderer line;
    [Tooltip("The distance between the camera and the cable while the players holding it")]
    [SerializeField] float lineHoldDist;
    [SerializeField] GameObject button;
    [SerializeField] List<Light> completionLights;
    TubeRenderer currentCable;
    // Bit shift the index of the layer (8) to get a bit mask
    int cableMask = 1 << 8;

    [SerializeField] Light passLight;
    [SerializeField] Light failLight;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;

        foreach (Light light in completionLights)
        {
            light.enabled = false;
        }
        //Invert the layer mask so that it targets everything apart from the layer its set to
        cableMask = ~cableMask;
    }

    private void Start()
    {
        GameEvents.current.puzzleCompleted += puzzleCompleted;
        GameEvents.current.puzzleReset += resetPuzzle;
    }

    void Update()
    {
        CheckForButtonPress();
        //if the switch is set to off
        if (!button.GetComponent<switchChanger>().getSwitchState())
        {
            RaycastHit hit;
            //if the user presses E while currently connecting a cable
            if (Input.GetButtonDown("Interact"))
            {
                if (inputCurrentlyConnecting)
                {
                    //just realised this is irrelevent. I'll fix after prototype is out
                    //Cam from week 6 here. Not fixing this
                    if (InteractRaycasting.Instance.raycastInteract(out hit, cableMask))
                    {
                        if (hit.transform.CompareTag("OutputNode"))
                        {
                            ConnectCable(hit);
                        }
                        else
                        {
                            //Destroy(inputCurrentlyConnecting.GetComponent<LineRenderer>());
                            currentCable.gameObject.SetActive(false);
                            currentCable = null;
                        }
                        inputCurrentlyConnecting = null;
                    }
                }

                if (InteractRaycasting.Instance.raycastInteract(out hit))
                {
                    //loop through all the input nodes, and if the raycast hit one of em, set up the line renderer and stuff
                    foreach (GameObject i in InputNodes)
                    {
                        if (hit.transform.gameObject == i)
                        {
                            inputCurrentlyConnecting = i;
                            inputCurrentlyConnecting.GetComponent<Node>().connectedNode = null;
                            //SetUpLine();
                            SetUpCable();
                        }
                    }
                    
                }
            }
            if (inputCurrentlyConnecting)
            {
                //DrawLine();
                UpdateCable();
            }
            else if (currentCable != null)
            {
                
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
                        if (failLight != null && passLight != null)
                        {
                            failLight.enabled = false;
                            passLight.enabled = true;
                        }
                        button.GetComponent<switchChanger>().TurnPowerOn();
                        foreach (Light light in completionLights)
                        {
                            light.enabled = true;
                        }
                        GameEvents.current.onPowerTurnedOn(id);
                        if(isSinglePuzzle)
                        {
                            GameEvents.current.onPuzzleComplete(id);

                            if (Analysis.current != null)
                            {
                                if (Analysis.current.consent)
                                {
                                    Analysis.current.resetTimer("Wires");
                                }
                            }
                        }
                        
                        
                    }
                    else
                    {
                        foreach (Light light in completionLights)
                        {
                            light.enabled = false;
                        }
                        GameEvents.current.onPowerTurnedOff(id);
                        button.GetComponent<switchChanger>().TurnPowerOff();
                        if(failLight != null && passLight != null)
                        {
                            failLight.enabled = true;
                            passLight.enabled = false;
                        }
                    }
                }
            }
        }
    }

    public void TurnOffLights()
    {
        foreach (Light light in completionLights)
        {
            light.enabled = false;
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

    void SetUpCable()
    {
        currentCable = inputCurrentlyConnecting.transform.GetChild(0).GetComponent<TubeRenderer>();
        currentCable.gameObject.SetActive(true);
    }

    void UpdateCable()
    {
        if (currentCable != null)
        {
            //I know this only works for a renderer with 2 proper points. I dont care. Waaaaay more work to even get 3 points working
            //the third position is just used to give the tube an end (so that u cant see inside of it)
            Vector3[] positions = new Vector3[3];
            positions[0] = currentCable.positions[0];
            positions[1] = (cam.position) + (lineHoldDist * cam.forward);
            positions[2] = positions[1];
            currentCable.worldPositions(positions);
            //ooooo magic numbers scaryyyyy
            //dont care
            currentCable.uvScale = new Vector2(positions[1].z, positions[1].z);
        }
    }

    void ConnectCable(RaycastHit hit)
    {
        //if the raycast hits an output node, lock the line renderer to it and update the input node so it knows
        //that its connected now
        //inputCurrentlyConnecting.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
        Vector3[] positions = new Vector3[3];
        positions[0] = currentCable.positions[0];
        positions[1] = hit.transform.position;
        positions[2] = positions[1];
        //i fucking hate how currentCable.positions is local pos
        currentCable.worldPositions(positions);
        inputCurrentlyConnecting.GetComponent<Node>().connectedNode = hit.transform.gameObject;
    }

    public void resetPuzzle(int id)
    {
        if(id == this.id)
        {
            foreach (GameObject inputNode in InputNodes)
            {
                Destroy(inputNode.GetComponent<LineRenderer>());
                TubeRenderer tb = inputNode.transform.GetChild(0).GetComponent<TubeRenderer>();
                Vector3[] pos = new Vector3[tb.positions.Length];
                for (int i = 0; i < tb.positions.Length; i++)
                {
                    pos[i] = Vector3.zero;
                }
                tb.SetPositions(pos);
                
                inputNode.GetComponent<Node>().connectedNode = null;
            }
            button.GetComponent<switchChanger>().TurnPowerOff();
            PuzzleData.current.completedEvents[id] = false;
            PuzzleData.current.isCompleted[id - 1] = false;
            passLight.enabled = false;
            failLight.enabled = false;
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
                Analysis.current.failCounterWires++;
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
                        /*line = InputNodes[i].AddComponent<LineRenderer>();*/
                        /*line.startWidth = lineWidth;
                        line.endWidth = lineWidth;
                        line.positionCount = 2;
                        line.material = InputNodes[i].GetComponent<Node>().lineMat;
                        line.useWorldSpace = true;*/
                    }
                    /*line.SetPosition(0, InputNodes[i].GetComponent<Node>().desiredNode.transform.position);
                    line.SetPosition(1, InputNodes[i].GetComponent<Node>().transform.position);
                    line.GetComponent<Node>().connectedNode = InputNodes[i].GetComponent<Node>().desiredNode.transform.gameObject;*/
                }

            }

            PuzzleData.current.completedEvents[id] = true;
            PuzzleData.current.isCompleted[id - 1] = true;
        }
    }
}
