using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWhenLookedAt : MonoBehaviour
{
    bool isGlowing;
    Material baseMaterial;
    [SerializeField] Material glowingMaterial;

    void Awake()
    {
        baseMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    //this just toggles the material between the glowing one and the base one
    public void ToggleGlowingMat()
    {
        isGlowing = !isGlowing;
        //if the glowing bool is true, set the material to be glowing, if its false, set it to the base material
        this.gameObject.GetComponent<MeshRenderer>().material = isGlowing ? glowingMaterial : baseMaterial;
    }
}
