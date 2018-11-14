using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject[] initCubes;

    private GameObject[][][] cubes; // 1st dimension - x, 2nd dimension - y, 3rd dimension - z

	// Use this for initialization
	void Start () {
        // Populate the 3D array with the given cubes
        int n = 0;
        cubes = new GameObject[3][][];
        for (int i = 0;i < 3;i++)
        {
            GameObject[][] cubes2D = new GameObject[3][];
            for (int j = 0;j < 3;j++)
            {
                GameObject[] cubes3D = new GameObject[3];
                for (int k = 0;k < 3;k++)
                {
                    cubes3D[k] = initCubes[n];
                    n++;
                }
                cubes2D[j] = cubes3D;
            }
            cubes[i] = cubes2D;
        }

        // Randomize the colors every second
        InvokeRepeating("RandomizeColors", 0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Randomize all colors on the cube
    void RandomizeColors()
    {
        // List of possible colors
        Color[] colors = new Color[4];
        colors[0] = new Color(1.0f, 0.0f, 0.0f, 1.0f); // Red
        colors[1] = new Color(0.0f, 1.0f, 0.0f, 1.0f); // Green
        colors[2] = new Color(0.0f, 0.0f, 1.0f, 1.0f); // Blue
        colors[3] = new Color(1.0f, 1.0f, 0.0f, 1.0f); // Yellow

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    GameObject cube = cubes[i][j][k];
                    MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
                    Material colorMat = renderer.materials[1];
                    int r = Random.Range(0, 4);
                    colorMat.color = colors[r]; // Choose a random color and change the color material to it
                }
            }
        }
    }
}
