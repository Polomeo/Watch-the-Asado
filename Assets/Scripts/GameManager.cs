using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool isGameOver = false;
    public int gameDifficulty;

    SpawnManager spawnManager;
    PlayerController playerController;
    AudioHandler audioHandler;

    [SerializeField] GameObject mainMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] Button restartButton;
    [SerializeField] List<GameObject> targets;

    int score;
    int targetsLeft;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialize the external relationships
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioHandler = GameObject.FindWithTag("MainCamera").GetComponent<AudioHandler>();

        isGameActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Game Loop Functions
    public void StartGame(int difficulty)
    {
        // Activate Game
        isGameActive = true;

        // Set difficulty
        gameDifficulty = difficulty;

        // Set the UI
        scoreText.text = "Score: " + 0;
        scoreText.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        
        // Set the count of food targets left
        targetsLeft = targets.Count;

        // Start spawning enemies
        StartCoroutine(spawnManager.SpawnDogs());


    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void EatTargetFood()
    {
        Debug.Log("Food eaten!");

        // Game Loop
        if (targetsLeft > 0)
        {
            // De-activates the GameObject in the list according to the count of targets left
            targets[targetsLeft - 1].gameObject.SetActive(false);
            targetsLeft -= 1;

            if (targetsLeft == 0)
            {
                GameOver();
            }
        }

        // Audio
        audioHandler.PlayAudioOnce(audioHandler.chowSoundFX);
       
    }

    public void GameOver()
    {
        // Game Loop
        isGameActive = false;
        isGameOver = true;

        // UI
        gameOverText.gameObject.SetActive(true);
        finalScoreText.SetText("Puntos: " + score);
        finalScoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Audio
        playerController.PlayAudioOnce(playerController.gameOverAudio);
        audioHandler.PlayAudioOnce(audioHandler.endGameAudio); // Stops the backround music
    }

    public void RestartGame()
    {
        // Re-loads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Audio Functions
}