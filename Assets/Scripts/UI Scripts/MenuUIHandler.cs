using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    GameObject[] mainMenuButtons;
    GameObject[] difficultyMenuButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Store the diferent menu's buttons in arrays.
        mainMenuButtons = GameObject.FindGameObjectsWithTag("UI_MainMenuButtons");
        difficultyMenuButtons = GameObject.FindGameObjectsWithTag("UI_DifficultyMenuButtons");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
