using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDamage : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private float enemyDamage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player Hit");
            _healthController.playerHealth = _healthController.playerHealth - enemyDamage;
            _healthController.UpdateHealth();
        }
    }
}