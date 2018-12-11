using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {

    public float fallSpeed; // Falling speed of bolt
    public GameObject[] circleSprites; // Array of Circle Sprite Objects to copy
    public Color[] colors; // Array of Colors

    private int rand; // Random number chosen for colors
    private Color color; // Random color chosen for bolt
    private GameObject circleSprite; // Random circle sprite chosen for bolt
    private RectTransform rectTransform; // Rect Transform of the Circle Sprite
	
    public void SetColor(int r)
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.materials[0].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        renderer.materials[0].color = colors[r]; // Choose a random color and change the color material to it
        color = colors[r];

        rand = r;
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
            rectTransform.sizeDelta = new Vector2(40 - 4 * (transform.position.y - 3), 40 - 4 * (transform.position.y - 3));
        }
    }

    void OnTriggerStay(Collider collision)
    {
        GameObject cube = collision.gameObject;
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        if (renderer.materials.Length > 1 && renderer.materials[1].color != color) // Get the color of the cube the bolt has collided with
        {
            Destroy(circleSprite);
            Destroy(this.gameObject); // If the colors are different, destroy the bolt
        }
    }
}
