using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {

    public float fallSpeed;

    private Color color;

	// Use this for initialization
	void Start () {
        Color[] colors = new Color[4];
        colors[0] = new Color(1.0f, 0.0f, 0.0f, 1.0f); // Red
        colors[1] = new Color(0.0f, 1.0f, 0.0f, 1.0f); // Green
        colors[2] = new Color(0.0f, 0.0f, 1.0f, 1.0f); // Blue
        colors[3] = new Color(1.0f, 1.0f, 0.0f, 1.0f); // Yellow

        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.materials[0].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        int r = Random.Range(0, 4);
        renderer.materials[0].color = colors[r]; // Choose a random color and change the color material to it
        color = colors[r];
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position - Time.deltaTime * fallSpeed * new Vector3(0, 1, 0); // Make the bolt fall at the fall speed
	}

    void OnTriggerStay(Collider collision)
    {
        GameObject cube = collision.gameObject;
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        if (renderer.materials.Length > 1 && renderer.materials[1].color != color) // Get the color of the cube the bolt has collided with
        {
            Destroy(this.gameObject); // If the colors are different, destroy the bolt
        }
    }
}
