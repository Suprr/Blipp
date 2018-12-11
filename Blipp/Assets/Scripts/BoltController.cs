using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {

    private float spawnHeight = LevelData.spawnHeight; // Spawning height of bolt
    private float fallSpeed = LevelData.fallSpeed; // Falling speed of bolt
    public GameObject[] circleSprites; // Array of Circle Sprite Objects to copy
    public GameController gamecontroller;

    private int rand; // Random number chosen for colors
    private Color color; // Random color chosen for bolt
    private GameObject circleSprite; // Random circle sprite chosen for bolt
    private RectTransform rectTransform; // Rect Transform of the Circle Sprite
	
    // Set the Color of the Bolt
    public void SetColor(int c)
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.materials[0].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        renderer.materials[0].color = LevelData.colors[c]; // Change the color material to the color given by the level
        color = LevelData.colors[c];
        rand = c; // Store the index of the color so we can find the corresponding circleSprite

        transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
    }

    // Create a Circle Sprite given the X and Y
    public void CreateCircleSprite(int x, int y)
    {
        GameObject newCircleSprite = Instantiate(circleSprites[rand]); // Copy the circle sprite
        newCircleSprite.transform.SetParent(circleSprites[rand].transform.parent);

        int newX = 86 + 49 * x; // Calculate the position on the UI relative to X and Y
        int newY = -1 * (86 - 49 * y);
        rectTransform = newCircleSprite.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(newX, newY, 0); // Change the position and initialize size
        rectTransform.sizeDelta = new Vector2(12, 12);

        newCircleSprite.SetActive(true); // Set Active
        circleSprite = newCircleSprite;
    }

	// Update is called once per frame
	void Update () {
        transform.position = transform.position - Time.deltaTime * fallSpeed * new Vector3(0, 1, 0); // Make the bolt fall at the fall speed
        if (transform.position.y >= 3) // If the bolt has not reached the cube, make the circle sprite grow in size based on how far the bolt has fallen
        {
            rectTransform.sizeDelta = new Vector2(40 - (40 / spawnHeight) * (transform.position.y - 3), 40 - (40 / spawnHeight) * (transform.position.y - 3));
        }
        if (transform.position.y <= -4)
        {
            gamecontroller.AddScore(); // Make the player gain score
            Destroy(circleSprite);
            Destroy(this.gameObject); // The bolt has made it through the cube so destroy it
        }
    }

    void OnTriggerStay(Collider collision)
    {
        GameObject cube = collision.gameObject;
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        if (renderer.materials.Length > 1 && renderer.materials[1].color != color) // Get the color of the cube the bolt has collided with
        {
            gamecontroller.LoseLife(); // Make the player lose a life
            Destroy(circleSprite);
            Destroy(this.gameObject); // If the colors are different, destroy the bolt
        }
    }
}
