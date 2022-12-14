using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = false;
    public bool isGameOver = false;
    public int gameDifficulty;

    SpawnManager spawnManager;
    AudioHandler audioHandler;

    [SerializeField] GameObject lifeCounter;
    [SerializeField] GameObject[] hearts;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TMP_InputField highscoreNameInput;
    [SerializeField] Button restartButton;
    [SerializeField] Button saveScoreButton;
    [SerializeField] List<GameObject> targets;

    int score;
    int targetsLeft;
    int livesLeft;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialize the external relationships
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        audioHandler = GameObject.FindWithTag("MainCamera").GetComponent<AudioHandler>();

        // Count the lives left
        livesLeft = hearts.Length;

        StartGame(MainManager.Instance.Difficulty);

    }

    // Game Loop Functions
    public void StartGame(int difficulty)
    {
        // Activate Game
        MainManager.Instance.IsGameActive = true;

        // Set difficulty
        MainManager.Instance.Difficulty = difficulty;

        // Set the UI
        scoreText.text = "Score: " + 0;
        scoreText.gameObject.SetActive(true);
        lifeCounter.gameObject.SetActive(true);
        
        // Set the count of food targets left
        targetsLeft = targets.Count;

        // Start the spawner
        StartSpawn();
    }
    private void StartSpawn()
    {
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

    public void PlayerHit()
    {
        livesLeft--;

        if (MainManager.Instance.IsGameActive)
        {
            hearts[livesLeft].gameObject.SetActive(false);

        }
        
        if(livesLeft == 0)
        {
            GameOver();

        }

    }

    public void GameOver()
    {
        // Game Loop
        MainManager.Instance.IsGameActive = false;
        MainManager.Instance.IsGameOver = true;

        // UI
        gameOverText.gameObject.SetActive(true);

        // Highscore
        if (score > MainManager.Instance.HighScore)
        {
            finalScoreText.SetText("Score: " + score + ".\n New Highscore!");
            highscoreNameInput.gameObject.SetActive(true);
            saveScoreButton.gameObject.SetActive(true);
            
        }
        else
        {
            finalScoreText.SetText("Points: " + score);
        }

        finalScoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Audio
        // playerController.PlayAudioOnce(playerController.gameOverAudio);
        audioHandler.PlayAudioOnce(audioHandler.endGameAudio); // Stops the backround music

        // Save Data
        MainManager.Instance.SaveHighScore(score, "Polo");
    }

    public void RestartGame()
    {
        // Re-loads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveScoreButton()
    {
        // Get the text from the input
        string playerName = highscoreNameInput.text;
        Debug.Log("Player name: " + playerName);

        // Save Data
        MainManager.Instance.SaveHighScore(score, playerName);

        // Load Data
        MainManager.Instance.LoadHighScore();

        // Disable button
        saveScoreButton.gameObject.SetActive(false);
    }

    // Audio Functions
}
