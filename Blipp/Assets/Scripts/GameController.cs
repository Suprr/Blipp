using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject bolt;
    public CubeController cubecontroller;
    public Text scoreText; // Score Text
    public Text livesText; // Lives Text
    public Text comboText; // Combo Text
    public int scoreValue; // Value that each bolt gives, scorewise

    private int boltCount; // Current bolt we are up to
    private int boltCompleteCount; // Current number of bolts we have completed
    private int score; // Current score
    private int combo; // Current combo
    private int lives; // Current lives
    private bool shiftUpColors; // True if colors need to be shifted up

    private float arcadeBoltTime;

	// Use this for initialization
	void Start () {
        score = 0;
        combo = 1;
        lives = 3;
        boltCount = 0;
        boltCompleteCount = 0;
        ScoreData.combo = 0;
        if (LevelData.arcade)
        {
            arcadeBoltTime = 8.4f;
            Invoke("SpawnArcadeBolt", 4.0f);
        } else
        { // Not arcade mode, spawn bolts according to level data
            for (int i = 0; i < LevelData.nBolts; i++)
            {
                Invoke("SpawnBolt", LevelData.boltTimes[i]); // Call SpawnBolt nBolts times, using the boltTimes to determine when to invoke it
            }
        }
	}

    private void Update()
    {
        if (!LevelData.arcade && lives > 0 && boltCompleteCount >= LevelData.nBolts) // If we have lives and have completed all bolts, we win the game
        {
            ScoreData.win = true; // Send final data to Score Data
            ScoreData.score = score;
            ScoreData.lives = lives;
            SceneManager.LoadScene("LevelEnd"); // Load LevelEnd screen
        }
    }

    // Spawn a new Bolt
    void SpawnBolt()
    {
        int c = LevelData.bolts[boltCount]; // Get the index of the color of the bolt we are up to
        int x = LevelData.boltPos[boltCount][0]; // Get the bolt position
        int y = LevelData.boltPos[boltCount][1];
        boltCount++; // Increment bolt count
        CreateBolt(c, x, y);
    }

    // Spawn an Arcade mode Bolt, which is completely random
    void SpawnArcadeBolt()
    {
        int c = Random.Range(0, LevelData.colors.Length);
        int x = 0;
        int y = 0;
        while (x == 0 && y == 0) // Keep randomizing x and y to make sure the bolt doesn't fall on the center cube
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }
        CreateBolt(c, x, y);
        if (arcadeBoltTime > 6.0f) // Modify arcadeBoltTime so that the arcade bolts spawn more frequently
        {
            arcadeBoltTime -= 0.4f;
        } else if (arcadeBoltTime > 4.0f)
        {
            arcadeBoltTime -= 0.2f;
        } else
        {
            LevelData.fallSpeed += 0.1f; // Increase speed after to prevent bolt flooding
            LevelData.spawnHeight += 1.0f;
        }
        boltCount++;
        if (boltCount % 5 == 0) // Every 5 bolts, shift down the colors on the cube and randomize the top level again
        {
            shiftUpColors = true;
        }
        Invoke("SpawnArcadeBolt", arcadeBoltTime); // Spawn another arcade bolt, continue infinitely
    }

    // Create a new Bolt GameObject and initialize it given a Color value and spawn coordinates
    void CreateBolt(int c, int x, int y)
    {
        GameObject newBolt = Instantiate(bolt); // Copy the bolt
        newBolt.transform.SetParent(bolt.transform.parent);
        newBolt.GetComponent<BoltController>().SetColor(c); // Pass the level-chosen values to the bolt
        newBolt.GetComponent<BoltController>().CreateCircleSprite(x, y);
        newBolt.transform.position = new Vector3(newBolt.transform.position.x + x * 2, newBolt.transform.position.y, newBolt.transform.position.z + y * 2); // Randomize the position
        // Color is randomized in BoltController Start()
        newBolt.SetActive(true); // Set the bolt to active
    }

    // Add to score after successful bolt
    public void AddScore()
    {
        score += scoreValue * combo; // Multiply score value by combo
        combo++; // Increment combo
        if (combo > ScoreData.combo)
        {
            ScoreData.combo = combo;
        }
        scoreText.text = "Score: " + score.ToString("000000"); // Update Score and Combo Text
        comboText.text = "Combo: " + combo.ToString() + "x";
        boltCompleteCount++;

        if (LevelData.arcade && shiftUpColors)
        {
            cubecontroller.ShiftUpColors();
            shiftUpColors = false;
        }
    }

    // Lose life due to missed bolt
    public void LoseLife()
    {
        lives--; // Decrement lives
        combo = 1; // Reset combo
        livesText.text = "Lives: " + lives; // Update lives and combo text
        comboText.text = "Combo: " + combo.ToString() + "x";
        if (lives == 2) // Change color of lives based on number of lives remaining
        {
            livesText.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        } else if (lives == 1)
        {
            livesText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        } else if (lives <= 0) // If we have no lives left, we lose the game
        {
            ScoreData.win = false; // Send final data to Score Data
            ScoreData.score = score;
            ScoreData.lives = 0;
            SceneManager.LoadScene("LevelEnd"); // Load LevelEnd screen
        }
        boltCompleteCount++;

        if (LevelData.arcade && shiftUpColors)
        {
            cubecontroller.ShiftUpColors();
            shiftUpColors = false;
        }
    }
}
