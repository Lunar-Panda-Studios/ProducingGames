using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    public string clipName;
    public Transform endPosition;
    private bool isActivated;
    private bool isLev = false;
    private Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if(isActivated)
        {
            SoundEffectManager.GlobalSFXManager.PlaySFX(clipName);
            if(isLev)Destroy(rb);
            Destroy(this);
        }
    }
    public void Fly(float force)
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = endPosition.position - transform.position;
        rb.AddForce(direction * force,ForceMode.Impulse);
        isActivated = true;
    }
    public void Levitate(float forceUp, float forceDown, float delay)
    {
        isLev = true;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
        StartCoroutine(DropObject(delay, forceDown));
    }
    public IEnumerator DropObject(float delay,float forceDown)
    {
        yield return new WaitForSeconds(delay);
        isActivated = true;
        rb.useGravity = true;
        rb.AddForce(Vector3.down * forceDown, ForceMode.Impulse);
    }
    public void SetIsActivated(bool value)
    {
        isActivated = value;
    }

}
