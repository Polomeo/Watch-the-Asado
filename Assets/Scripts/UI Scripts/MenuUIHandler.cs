using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject difficultyMenu;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        // Store the diferent menus and buttons
        mainMenu = GameObject.Find("Main Menu");
        difficultyMenu = GameObject.Find("Difficulty Menu");
        quitButton = GameObject.Find("Quit Button");
        backButton = GameObject.Find("Back Button");

        // Hide the ones not in use
        DeactivateMenu(difficultyMenu);
        DeactivateMenu(backButton);

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
        DeactivateMenu(backButton);

        ActivateMenu(mainMenu);
        ActivateMenu(quitButton);
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
