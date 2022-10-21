using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    [SerializeField] float xSpawnRange = 35f;
    [SerializeField] float zSpawnRange = 20f;

    [SerializeField] float minSpawnTimerSec;
    [SerializeField] float maxSpawnTimerSec;
    
    float spawnTimer;
    GameManager gameManager;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // InvokeRepeating("SpawnEnemy", 0.5f, spawnTimer);
    }

    public IEnumerator SpawnDogs()
    {
        while (gameManager.isGameActive)
        {
            // The time between spawns is divided by the difficulty (+ difficulty, - time between spawns)
            spawnTimer = Random.Range(minSpawnTimerSec, maxSpawnTimerSec) / gameManager.gameDifficulty;

            Debug.Log("Difficulty: " + gameManager.gameDifficulty);

            Debug.Log("Spawn Timer: " + spawnTimer);

            yield return new WaitForSeconds(spawnTimer);

            // Select a random Enemy
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);

            // Instantiate such enemy
            Instantiate(enemyPrefabs[enemyIndex], SpawnAroundPos(), enemyPrefabs[enemyIndex].transform.rotation);
        }
        


    }

    private Vector3 SpawnAroundPos()
    {
        float yOffset = 1.5f;

        // Set the Vector3 for posible spawn locations at each border of the screen
        Vector3 topSpawn = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), yOffset, zSpawnRange);
        Vector3 bottomSpawn = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), yOffset, -zSpawnRange);
        Vector3 rightSpawn = new Vector3(xSpawnRange, yOffset, Random.Range(-zSpawnRange, zSpawnRange));
        Vector3 leftSpawn = new Vector3(-xSpawnRange, yOffset, Random.Range(-zSpawnRange, zSpawnRange));

        // Create an array of posible spawn points
        Vector3[] spawns = { topSpawn, bottomSpawn, rightSpawn, leftSpawn};
        
        // Spawn from random spawn point arrond
        Vector3 spawnPos = spawns[Random.Range(0, spawns.Length)];

        return spawnPos;
    }
}
