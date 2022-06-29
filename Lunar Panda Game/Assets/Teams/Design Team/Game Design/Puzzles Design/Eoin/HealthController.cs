using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public float playerHealth;
    [SerializeField] private float _time = 3f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;
    }

    public void UpdateHealth()
    {
        //Debug.Log("UpdateHealth");
        if (playerHealth <= 0)
        {
            StartCoroutine(Respawn(_time));

        }
    }
    public IEnumerator Respawn(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene("Train v1.8 1");
    }
}