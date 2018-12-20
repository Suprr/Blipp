using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour {

    public Button levelOne;
    public Button levelTwo;
    public Button back;

	// Use this for initialization
	void Start () {
        levelOne.onClick.AddListener(LevelOne); // Add listener to button
        levelTwo.onClick.AddListener(LevelTwo); // Add listener to button
        back.onClick.AddListener(Back);
    }
	
    void LevelOne()
    {
        LevelData.LevelOne();
        SceneManager.LoadScene("Main"); // Load the main scene with the level data to play
    }

    void LevelTwo()
    {
        LevelData.LevelTwo();
        SceneManager.LoadScene("Main"); // Load the main scene with the level data to play
    }

    void Back() {
        SceneManager.LoadScene("MainMenu"); // Go back to Main Menu
    }
}
