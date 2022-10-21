using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    GameManager gameManager;
    AudioHandler audioHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioHandler = GameObject.FindWithTag("MainCamera").GetComponent<AudioHandler>();
        
        // After 5 seconds it auto-destroys
        StartCoroutine(DestroyAfterSeconds(5));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Updates the score based on Enemy public points variable.
            gameManager.UpdateScore(collision.gameObject.GetComponent<EnemyGhoul>().points);

            // Destroys the enemy
            Destroy(collision.gameObject);

            // Destroys the proyectile
            Destroy(gameObject);

        }
    }

    IEnumerator DestroyAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
