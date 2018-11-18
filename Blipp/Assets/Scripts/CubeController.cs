using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject cube;
    public float rotateSpeed;
    public GameObject[] initCubes;

    private GameObject[][][] cubes; // 1st dimension - x, 2nd dimension - y, 3rd dimension - z
    private bool rotating; // True if cube is rotating, false otherwise
    private float rotateProgress; // Progress in degrees of cube rotation so far
    private Vector3 rotateDir; // Direction to apply rotation in

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

        // Change the border of one cube to be white to represent the selected or "cursor" cube
        GameObject cube = cubes[1][2][0];
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        Material colorMat = renderer.materials[0];
        colorMat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        PerformRotation();
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

    // Perform the rotation animation on the cube if it is necessary
    void PerformRotation()
    {
        if (rotating) // Check if the cube is rotating
        {
            float newRotate = rotateSpeed * Time.deltaTime; // Generate the necessary rotation to perform
            rotateProgress += newRotate;
            if (rotateProgress >= 90f) // If we have rotated past 90 degrees, stop rotating
            {
                // Set the rotation back to exactly 90 degrees
                cube.transform.RotateAround(Vector3.zero, rotateDir, newRotate + 90f - rotateProgress);
                rotating = false;
            }
            else
            { // Otherwise apply the rotation
                cube.transform.RotateAround(Vector3.zero, rotateDir, newRotate);
            }
        }
    }

    public void RotateUp()
    {
        if (!rotating)
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(1, 0, 0);
        }
    }

    public void RotateLeft()
    {
        if (!rotating)
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(0, 1, 0);
        }
    }

    public void RotateDown()
    {
        if (!rotating)
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(-1, 0, 0);
        }
    }

    public void RotateRight()
    {
        if (!rotating)
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(0, -1, 0);
        }
    }

    public void CursorUp()
    {
        Debug.Log("Cursor Up");
    }

    public void CursorLeft()
    {
        Debug.Log("Cursor Left");
    }

    public void CursorDown()
    {
        Debug.Log("Cursor Down");
    }

    public void CursorRight()
    {
        Debug.Log("Cursor Right");
    }

    public void ShiftUp()
    {
        Debug.Log("Shift Up");
    }

    public void ShiftLeft()
    {
        Debug.Log("Shift Left");
    }

    public void ShiftDown()
    {
        Debug.Log("Shift Down");
    }

    public void ShiftRight()
    {
        Debug.Log("Shift Right");
    }
}
