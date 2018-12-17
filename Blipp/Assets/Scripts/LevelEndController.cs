using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndController : MonoBehaviour {

    public Text winLoseText;
    public Text scoreText;
    public Text comboText;
    public Text livesText;
    public Button playAgain;
    public Button mainMenu;

	// Use this for initialization
	void Start () {
		if (ScoreData.win) // Check if we won or lost, set text
        {
            winLoseText.text = "You Win!";
            winLoseText.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        } else
        {
            winLoseText.text = "You Lose!";
            winLoseText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        // Load score, combo data
        scoreText.text = "Score: " + ScoreData.score;
        scoreText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        comboText.text = "Best Combo: " + ScoreData.combo;
        comboText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        livesText.text = "Lives Remaining: " + ScoreData.lives;
        if (ScoreData.lives >= 3) // Load lives remaining and color based on number of lives
        {
            livesText.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        } else if (ScoreData.lives == 2)
        {
            livesText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        } else
        {
            livesText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        playAgain.onClick.AddListener(PlayAgain); // Add listener to button
    }

    void PlayAgain()
    {
        SceneManager.LoadScene("Main"); // Reload the main scene with the same level data to play again
    }
}
