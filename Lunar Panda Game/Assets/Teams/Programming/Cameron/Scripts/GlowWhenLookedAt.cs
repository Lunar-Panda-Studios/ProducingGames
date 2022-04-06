using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] ToolTipType tooltip;
    [SerializeField] Text tooltipTxt;
    [SerializeField] CanvasGroup tooltipGroup;
    [SerializeField] float waitTime = 0.5f;

    bool isFading = false;

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
        if (isGlowing && !isFading)
        {
            isFading = true;
            StartCoroutine(FadeTooltips());
        }
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

    IEnumerator FadeTooltips()
    {
        tooltipTxt.text = tooltip.text;
        for (float t = 0f; t < tooltip.fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / tooltip.fadeTime;
            tooltipGroup.alpha = Mathf.Lerp(0, 1, normalizedTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        while(InteractRaycasting.Instance.raycastInteract(out RaycastHit hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                yield return new WaitForEndOfFrame();
            }
            break;
        }

        for (float t = 0f; t < tooltip.fadeTime; t += Time.deltaTime)
        {
            float normalizedTime = t / tooltip.fadeTime;
            tooltipGroup.alpha = Mathf.Lerp(1, 0, normalizedTime);
            yield return null;
        }
        isFading = false;
        yield return null;
    }
}
