using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhoul : MonoBehaviour
{
    public float speed;
    public int points;
    
    GameManager gameManager;

    Rigidbody rb;
    GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        

        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Target");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowTarget();
    }


    //If hits target
    private void OnTriggerEnter(Collider other)
    {
        // If hits target
        if (other.gameObject.CompareTag("Target"))
        {

            Debug.Log("Target reached!");

            // Lowers the score
            if (!MainManager.Instance.IsGameOver)
            {
                gameManager.UpdateScore(-10);

            }

            // Eats a steak
            gameManager.EatTargetFood();

            // Destroys the GameObject
            Destroy(gameObject);

            
        }
        
        // If hits player
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");

            if (!MainManager.Instance.IsGameOver)
            {
                gameManager.UpdateScore(-10);

            }

            Destroy(gameObject);

            // Audio
            other.GetComponent<PlayerController>().GetBiten();
        }
    }

    private void FollowTarget()
    {
        // Move to target
        Vector3 lookDirection = (target.transform.position - transform.position).normalized;
        rb.AddForce(lookDirection * (speed));

        // Look at target (horizontaly)
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}

