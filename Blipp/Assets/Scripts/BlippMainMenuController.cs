using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlippMainMenuController : MonoBehaviour {

    public Button arcade;
    public Button levels;

    // Use this for initialization
    void Start() {
        arcade.onClick.AddListener(Arcade); // Add listener to button
        levels.onClick.AddListener(Levels); // Add listener to button
    }

    void Arcade()
    {
        LevelData.ArcadeMode();
        SceneManager.LoadScene("Main"); // Reload the main scene with the same level data to play again
    }
    void Levels()
    {
        SceneManager.LoadScene("LevelMenu"); // Switch to Level Menu
    }
}
