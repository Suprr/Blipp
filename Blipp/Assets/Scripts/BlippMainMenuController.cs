using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlippMainMenuController : MonoBehaviour {

    public Text welcomeText;
    // public Text scoreText;
    // public Text comboText;
    // public Text livesText;
    public Button arcade;
    public Button levels;

    // Use this for initialization
    void Start() {
        welcomeText.text = "Welcome to Blipp!";
        //     welcomeText.text = "You Win!";
        welcomeText.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        // } else
        // {
        //     winLoseText.text = "You Lose!";
        //     winLoseText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        // }

        // Load score, combo data
        // scoreText.text = "Score: " + ScoreData.score;
        // scoreText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        // comboText.text = "Best Combo: " + ScoreData.combo;
        // comboText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        // if (LevelData.arcade)
        // {
        //     livesText.text = "";
        // }
        // else
        // {
        //     livesText.text = "Lives Remaining: " + ScoreData.lives;
        // }
        // if (ScoreData.lives >= 3) // Load lives remaining and color based on number of lives
        // {
        //     livesText.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        // } else if (ScoreData.lives == 2)
        // {
        //     livesText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        // } else
        // {
        //     livesText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        // }

        arcade.onClick.AddListener(Arcade); // Add listener to button
        levels.onClick.AddListener(Levels); // Add listener to button
    }

    void Arcade()
    {
        LevelData.arcade=true;
        SceneManager.LoadScene("Main"); // Reload the main scene with the same level data to play again
    }
    void Levels()
    {
        LevelData.arcade=false;
        SceneManager.LoadScene("Main"); // Reload the main scene with the same level data to play again
    }
}
