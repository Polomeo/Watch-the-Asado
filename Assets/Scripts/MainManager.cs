using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.IO;

public class MainManager : MonoBehaviour
{
    // [SINGLETON] Main Manager
    public static MainManager Instance;

    public int Difficulty;
    public bool IsGameActive = false;
    public bool IsGameOver = false;
    public int Score;

    public int HighScore;
    public string HighScoreName;

    private void Awake()
    {
        if (Instance != null)
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


    // Serializable class for saving data
    [System.Serializable]
    class SaveData
    {
        public int HighScoreSv;
        public string HighScoreNameSv;
    }

    public void SaveHighScore(int hs, string playerName)
    {
        SaveData data = new SaveData();
        data.HighScoreSv = hs;
        data.HighScoreNameSv = playerName;

        // JSON functions to store data on persistent data path
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScoreSv;
            HighScoreName = data.HighScoreNameSv;
        }
    }

}
