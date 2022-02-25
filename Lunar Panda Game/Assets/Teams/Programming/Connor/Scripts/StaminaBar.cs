using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [Tooltip("maximum stamina value")]
    public float maxStam = 100f;
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
        if(currentStam - amount >= 0) // checks to see if you have enough stamina to perform action
        {
            currentStam -= amount;
            UIManager.Instance.ChangeStaminaUsage(currentStam);

            if(regenCr != null)
            {
                StopCoroutine(regenCr);
            }

            regenCr = StartCoroutine(stamRegen());
        }
    }

    private IEnumerator stamRegen()
    {
        yield return new WaitForSeconds(regenDelay);  

        while(currentStam < maxStam)
        {
            currentStam += maxStam / regenAmount;
            UIManager.Instance.ChangeStaminaUsage(currentStam);

            yield return regenPerFrame;
        }
        regenCr = null;
    }

}
