using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyController : MonoBehaviour
{
    public string AudioClip;
    private float audioClipLenght;

    public int id;
    [Header("Puzzle Values")]
    [Tooltip("The ID of this patient")]
    public int patientID;
    [Tooltip("Whether this patient is the one with the screwdriver or not")]
    public bool isCorrect;

    [Header("Meshes")]
    [Tooltip("The mesh of the body when it has been cut")]
    public Mesh cutBody;
    Mesh startMesh;
    private MeshFilter mesh;

    private bool isCut;

    [Header("Item Data")]
    [Tooltip("Item data for the scalpel")]
    public ItemData scalpelData;
    [Tooltip("Screwdriver game object (only needed for the body with the screwdriver)")]
    public GameObject screwdriverTip;
    private bool collected = false;
    private GameObject player;
    private Transform cam;

    private Inventory inventoryScript;

    void Awake()
    {
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        mesh = GetComponent<MeshFilter>();
        startMesh = mesh.mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryScript = FindObjectOfType<Inventory>();
        isCut = false;
        GameEvents.current.puzzleCompleted += puzzleCompleted;
        GameEvents.current.puzzleReset += puzzleReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, player.GetComponent<PlayerPickup>().pickupDist))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (inventoryScript.itemInventory[inventoryScript.selectedItem] != null)
                    {
                        if (inventoryScript.itemInventory[inventoryScript.selectedItem] == scalpelData)
                        {
                            //Cutscene or animation or whatever will go here
                            //StartCoroutine(OpenBody(AudioClip.Lenght))
                            changeMesh();
                            isCut = true;
                            if (isCorrect == true)
                            {
                                if (collected == false)
                                {
                                    screwdriverTip.SetActive(true);
                                    collected = true;

                                    PuzzleData.current.isCompleted[id - 1] = true;
                                    GameEvents.current.onPuzzleComplete(id);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private IEnumerator OpenBody(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        mesh.mesh = cutBody;
    }
    public void changeMesh()
    {
        mesh.mesh = cutBody;
    }

    public void puzzleReset(int id)
    {
        if(id == this.id)
        {
            collected = false;
            isCut = false;
            mesh.mesh = startMesh;
            if(screwdriverTip != null)
            {
                screwdriverTip.SetActive(false);
            }
        }
    }

    public void puzzleCompleted(int id)
    {
        if (id == this.id)
        {
            if(!isCut)
            {
                collected = true;
                isCut = true;
                if (isCorrect)
                {
                    mesh.mesh = cutBody;
                    screwdriverTip.SetActive(true);
                }
            }

            if (Analysis.current != null)
            {
                if (Analysis.current.consent && (!Analysis.current.timersPuzzlesp1.ContainsKey("Open Body") && !Analysis.current.timersPuzzlesp2.ContainsKey("Open Body")))
                {
                    Analysis.current.resetTimer("Open Body");
                }
            }
        }
    }
}
