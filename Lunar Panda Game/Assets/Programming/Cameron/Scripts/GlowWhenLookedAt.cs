using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWhenLookedAt : MonoBehaviour
{
    [SerializeField] bool isGlowing;
    Material baseMaterial;
    [SerializeField] Material glowingMaterial;

    void Awake()
    {
        baseMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void ToggleGlowingMat()
    {
        isGlowing = !isGlowing;
        this.gameObject.GetComponent<MeshRenderer>().material = isGlowing ? glowingMaterial : baseMaterial;
    }
}
