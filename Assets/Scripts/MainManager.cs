using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainManager : MonoBehaviour
{
    // [SINGLETON] Main Manager
    public static MainManager Instance;

    public int Difficulty;
    public bool IsGameActive = false;
    public bool IsGameOver = false;
    public int Score;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameDifficulty(int difficulty)
    {
        // Sets the difficulty
        Difficulty = difficulty;
        Debug.Log("Difficulty set to " + difficulty);

        // Launches the game scene
        SceneManager.LoadScene(1);
    }
}
