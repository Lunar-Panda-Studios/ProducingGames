using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWhenLookedAt : MonoBehaviour
{
    [HideInInspector] public bool isGlowing;
    [HideInInspector] public Material baseMaterial; //need this for the input script thingie but dont want in inspector
    [SerializeField] public Material glowingMaterial;
    [Header("Children")]
    [Tooltip("Only use this if the object you want to make glow has children that also need to glow")]
    [SerializeField] List<MeshRenderer> childrenThatNeedGlow;
    [SerializeField] List<Material> childrenFresnelMat;
    List<Material> childrenBaseMat = new List<Material>();

    void Awake()
    {
        baseMaterial = gameObject.GetComponent<MeshRenderer>().material;
        for (int i = 0; i < childrenThatNeedGlow.Count; i++)
        {
            childrenBaseMat.Add(childrenThatNeedGlow[i].material);
        }
    }

    //this just toggles the material between the glowing one and the base one
    public void ToggleGlowingMat()
    {
        isGlowing = !isGlowing;
        //if the glowing bool is true, set the material to be glowing, if its false, set it to the base material
        this.gameObject.GetComponent<MeshRenderer>().material = isGlowing ? glowingMaterial : baseMaterial;
        if(childrenThatNeedGlow.Count > 0)
        {
            for (int i = 0; i < childrenThatNeedGlow.Count; i++)
            {
                childrenThatNeedGlow[i].material = isGlowing ? childrenFresnelMat[i] : childrenBaseMat[i];
            }
        }
    }
}
