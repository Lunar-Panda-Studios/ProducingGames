using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [Tooltip("maximum stamina value")]
    public float maxStam = 100f;
    public bool CanSprint;
    public Transform ShowStaminaPos;
    public Transform HideStaminaPos;
    public GameObject Staminabarrr;
    public float currentStam;
    [Tooltip("delay before player starts regaining stamina")]
    public float regenDelay = 1f;
    [Tooltip("higher the number the smaller the regeneration")]
    public float regenAmount = 100f;


    public static StaminaBar instance;

    private WaitForSeconds regenPerFrame = new WaitForSeconds(0.1f);
    private Coroutine regenCr;

    UIManager uimanager;

    private void Awake()
    {
        instance = this;
        uimanager = UIManager.Instance;
    }

    void Start()
    {
        currentStam = maxStam;
    }

    public void staminaUsage(float amount)
    {
        if(currentStam - amount >= 0 && CanSprint == true) // checks to see if you have enough stamina to perform action
        {
            currentStam -= amount;
            UIManager.Instance.ChangeStaminaUsage(currentStam);

            if (regenCr != null)
            {
                StopCoroutine(regenCr);
            }

            regenCr = StartCoroutine(stamRegen());
        }
    }

    private IEnumerator stamRegen()
    {
        yield return new WaitForSeconds(regenDelay);

        while(currentStam < maxStam && CanSprint == true)
        {
            currentStam += maxStam / regenAmount;
            UIManager.Instance.ChangeStaminaUsage(currentStam);

            yield return regenPerFrame;
        }
        regenCr = null;
    }


    private void Update()
    {
        if (CanSprint == false)
        {
            Staminabarrr.transform.position = HideStaminaPos.transform.position;
        }

        if (CanSprint == true)
        {
            Staminabarrr.transform.position = ShowStaminaPos.transform.position;
        }
    }

}