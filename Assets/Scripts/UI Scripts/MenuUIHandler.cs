using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject difficultyMenu;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject highScoreButton;
    [SerializeField] GameObject highScoreMenu;
    [SerializeField] TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Store the diferent menus and buttons
        mainMenu = GameObject.Find("Main Menu");
        difficultyMenu = GameObject.Find("Difficulty Menu");
        quitButton = GameObject.Find("Quit Button");
        backButton = GameObject.Find("Back Button");
        highScoreMenu = GameObject.Find("Highscore Menu");

        // Hide the ones not in use
        DeactivateMenu(difficultyMenu);
        DeactivateMenu(highScoreMenu);
        DeactivateMenu(backButton);

        // Sets the Highscore
        MainManager.Instance.LoadHighScore();

        string highScore = MainManager.Instance.HighScore.ToString();
        string highScoreName = MainManager.Instance.HighScoreName;

        highScoreText.SetText(highScoreName + " - " + highScore + " points.");

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        ActivateMenu(difficultyMenu);
        ActivateMenu(backButton);

        DeactivateMenu(mainMenu);
        DeactivateMenu(quitButton);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void BackButton()
    {
        DeactivateMenu(difficultyMenu);
        DeactivateMenu(highScoreMenu);
        DeactivateMenu(backButton);


        ActivateMenu(mainMenu);
        ActivateMenu(quitButton);
    }

    public void HighScoreButton()
    {
        DeactivateMenu(mainMenu);
        DeactivateMenu(quitButton);

        ActivateMenu(backButton);
        ActivateMenu(highScoreMenu);
        ActivateMenu(backButton);
    }

    private void DeactivateMenu(GameObject menu)
    {
        // Deactivates the menuButtons
        menu.gameObject.SetActive(false);
        
    }

    private void ActivateMenu(GameObject menu)
    {
        // Activates the menuButtons
        menu.gameObject.SetActive(true);
    }

}
