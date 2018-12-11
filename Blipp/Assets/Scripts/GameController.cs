using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject bolt; // Original bolt game object
    public int nBolts; // Number of bolts
    public int timeBetweenBolts; // Time between bolts
    public Text scoreText; // Score Text
    public Text livesText; // Lives Text
    public Text comboText; // Combo Text
    public int scoreValue; // Value that each bolt gives, scorewise

    private int score; // Current score
    private int combo; // Current combo
    private int lives; // Current lives

	// Use this for initialization
	void Start () {
        score = 0;
        combo = 1;
        lives = 3;
		for (int i = 0;i < nBolts;i++)
        {
            Invoke("SpawnBolt", timeBetweenBolts * i); // Call SpawnBolt nBolts times, with a delay of timeBetweenBolts between each call
        }
	}

    // Spawn a new Bolt
    void SpawnBolt()
    {
        GameObject newBolt = Instantiate(bolt); // Copy the bolt
        newBolt.transform.SetParent(bolt.transform.parent);
        int r1 = Random.Range(-1, 2); 
        int r2 = Random.Range(-1, 2);
        int r = Random.Range(0, 4); // Choose a new random color
        newBolt.GetComponent<BoltController>().SetColor(r); // Pass the randomly chosen values to the bolt
        newBolt.GetComponent<BoltController>().CreateCircleSprite(r1, r2);
        newBolt.transform.position = new Vector3(newBolt.transform.position.x + r1 * 2, newBolt.transform.position.y, newBolt.transform.position.z + r2 * 2); // Randomize the position
        // Color is randomized in BoltController Start()
        newBolt.SetActive(true); // Set the bolt to active
    }

    // Add to score after successful bolt
    public void AddScore()
    {
        score += scoreValue * combo; // Multiply score value by combo
        combo++; // Increment combo
        scoreText.text = "Score: " + score.ToString("000000"); // Update Score and Combo Text
        comboText.text = "Combo: " + combo.ToString() + "x";
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
        }
    }
}
