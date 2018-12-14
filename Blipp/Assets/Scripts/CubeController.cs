using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject cube;
    public GameObject cursor;
    public GameObject[] initCubes;
    public CameraController cameracontroller;

    private GameObject[][][] cubes; // 1st dimension - x, 2nd dimension - y, 3rd dimension - z
    private int[] cursorLoc; // Current cursor position, 1st dimension - x, from 0 to 2, 2nd dimension - z or y from 0 to 5
    private bool cursorFront; // Current cursor face

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
        // InvokeRepeating("RandomizeColors", 0.0f, 1.0f);
        // RandomizeColors();
        InitializeColors();

        // Set the default position of the cursor to be the center top cube
        cursorLoc = new int[2];
        cursorLoc[0] = 1;
        cursorLoc[1] = 1;
        cursorFront = false;
        UpdateCursor(cursorLoc);
    }

    // Initialize the colors based on LevelData
    void InitializeColors()
    {
        for (int i = 0;i < 3;i++)
        {
            for (int j = 0;j < 3;j++)
            {
                for (int k = 0;k < 3;k++)
                {
                    GameObject cube = cubes[i][j][k];
                    MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
                    MeshRenderer[] childRenderer = cube.GetComponentsInChildren<MeshRenderer>();
                    Debug.Log(childRenderer.Length);


                    Material colorMat = renderer.materials[0];
                    Material colorMatChild = childRenderer[1].materials[0];
                    colorMatChild.color = LevelData.cubeColors[i][j][k]; 
                    colorMat.color = LevelData.cubeColors[i][j][k]; // Set the color of the cube to the defined color in level data
                    // colorMat.color.SetFloat("_Mode", 4f);
                    // Color32 col = LevelData.cubeColors[i][j][k];
                    // col.a = 0;
                    // renderer.material.SetColor("_Color", col);
                }
            }
        }
    }

    // Renders the cursor by setting its border to white and all other borders to black
    /*
    public void RenderCursor()
    {
        for (int i = 0;i < 3;i++)
        {
            for (int j = 0;j < 3;j++)
            {
                for (int k = 0;k < 3;k++)
                {
                    GameObject cube = cubes[i][j][k];
                    MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
                    Material colorMat = renderer.materials[0];
                    if (i == cursorLoc[0] && j == cursorLoc[1] && k == cursorLoc[2])
                    {
                        colorMat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    } else
                    {
                        colorMat.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                    }
                }
            }
        }
    }*/ 

    // Randomize all colors on the cube
    void RandomizeColors()
    {
        // List of possible colors
        Color[] colors = new Color[4];
        colors[0] = new Color(1.0f, 0.0f, 0.0f, 0.0f); // Red
        colors[1] = new Color(0.0f, 1.0f, 0.0f, 0.0f); // Green
        colors[2] = new Color(0.0f, 0.0f, 1.0f, 0.0f); // Blue
        colors[3] = new Color(1.0f, 1.0f, 0.0f, 0.0f); // Yellow

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

    // Check if the new cursor location is within the cursor bounds
    bool CheckCursorBounds(int[] newCursorLoc)
    {
        if (newCursorLoc[0] > 2 || newCursorLoc[0] < 0) // Ensure it is within the x-bounds
        {
            return false;
        }
        if (newCursorLoc[1] > 5 || newCursorLoc[1] < 0) // Ensure it is within the yz-bounds
        {
            return false;
        }
        return !cameracontroller.GetUndoRotate(); // Return true if the camera is not rotating
    }

    // Update the cursor position given a new, valid, cursor location
    void UpdateCursor(int[] newCursorLoc)
    {
        cursorLoc[0] = newCursorLoc[0]; // Update cursor location
        cursorLoc[1] = newCursorLoc[1];
        if (cursorLoc[1] > 2) // If we have traveled more than 2 in the yz-bounds, we are on the front face now
        {
            if (!cursorFront) // If we are transitioning from the top face to the front face, rotate the cursor
            {
                cursor.transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), 270);
                cursor.transform.RotateAround(Vector3.zero, new Vector3(0, 0, 1), 90);
            }
            cursorFront = true; // Parse the cursor position using the cursor location
            cursor.transform.position = new Vector3(-2 + cursorLoc[0] * 2, 2 - (cursorLoc[1] - 3) * 2, -3.01f);
        }
        else // If we have traveled less than or equal to 2 in the yz-bounds, we are on the front face now
        {
            if (cursorFront) // If we are transitioning from the front face to the top face, rotate the cursor
            {
                cursor.transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), -270);
                cursor.transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), -90);
            }
            cursorFront = false; // Parse the cursor position using the cursor location
            cursor.transform.position = new Vector3(-2 + cursorLoc[0] * 2, 3.01f, 2 - cursorLoc[1] * 2);
        }
    }

    // Move the cursor up
    public void CursorUp()
    {
        int[] newCursorLoc = (int[]) cursorLoc.Clone();
        newCursorLoc[1]--; // Update new cursor location by moving it up
        if (CheckCursorBounds(newCursorLoc)) // Make sure the new cursor location is valid
        {
            UpdateCursor(newCursorLoc); // Update the cursor location
        }
    }

    // Move the cursor left
    public void CursorLeft()
    {
        int[] newCursorLoc = (int[])cursorLoc.Clone();
        newCursorLoc[0]--; // Update new cursor location by moving it left
        if (CheckCursorBounds(newCursorLoc)) // Make sure the new cursor location is valid
        {
            UpdateCursor(newCursorLoc); // Update the cursor location
        }
    }

    // Move the cursor down
    public void CursorDown()
    {
        int[] newCursorLoc = (int[])cursorLoc.Clone();
        newCursorLoc[1]++; // Update new cursor location by moving it down
        if (CheckCursorBounds(newCursorLoc)) // Make sure the new cursor location is valid
        {
            UpdateCursor(newCursorLoc); // Update the cursor location
        }
    }

    // Move the cursor right
    public void CursorRight()
    {
        int[] newCursorLoc = (int[])cursorLoc.Clone();
        newCursorLoc[0]++; // Update new cursor location by moving it right
        if (CheckCursorBounds(newCursorLoc)) // Make sure the new cursor location is valid
        {
            UpdateCursor(newCursorLoc); // Update the cursor location
        }
    }

    // Set the color of a cube at the given Vector coordinates
    void SetColor(Vector3 coords, Color color)
    {
        GameObject cube = cubes[(int)Mathf.Round(coords[0])][(int)Mathf.Round(coords[1])][(int)Mathf.Round(coords[2])]; // Get cube at Vector coordinates by rounding
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>(); // Get mesh renderer to get color material
        MeshRenderer[] childRenderer = cube.GetComponentsInChildren<MeshRenderer>();
        Material colorMat = renderer.materials[0];
        Material colorMatChild = childRenderer[1].materials[0];
        colorMatChild.color = color;
        colorMat.color = color; // Set color
    }

    // Get the color of a cube at the given Vector coordinates
    Color GetColor(Vector3 coords)
    {
        GameObject cube = cubes[(int) Mathf.Round(coords[0])][(int)Mathf.Round(coords[1])][(int)Mathf.Round(coords[2])]; // Get cube at Vector coordinates by rounding
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>(); // Get mesh renderer to get color material
        Material colorMat = renderer.materials[0];
        return colorMat.color; // Return color
    }

    // Shift the cubes in a certain direction around the cube
    // x, y, z - starting coordinates
    // Note that an axis in this algorithm is 0 - x, 1 - y, 2 - z
    // constantAxis - axis that is not changed during shifting, so the shift is completely independent of this axis
    // startChangingAxis - axis that is initially changed (so shifted towards) in the start of the algorithm
    // startOtherAxis - other axis that is neither always constant nor the starting change axis, so it is temporarily constant
    // initDirection - initial direction that the changing axis changes by, so either +1 or -1
    void Shift(int constantAxis, int startChangingAxis, int startOtherAxis, int initDirection)
    {
        if (cameracontroller.GetUndoRotate()) // If shifting is not allowed, do nothing
        {
            return;
        }

        // Obtain the cube that the cursor is on using the cursor location and cursor face
        int x;
        int y;
        int z;
        if (cursorFront)
        {
            x = cursorLoc[0];
            y = 5 - cursorLoc[1];
            z = 0;
        } else
        {
            x = cursorLoc[0];
            y = 2;
            z = 2 - cursorLoc[1];
        }

        Vector3 start = new Vector3(x, y, z); // Starting vector
        bool done = false; // True when we are done shifting
        int changingAxis = startChangingAxis; // Current changing axis
        int otherAxis = startOtherAxis; // Current other axis
        int direction = initDirection; // Current direction
        Vector3[] shifts = new Vector3[8]; // Array of cubes in this shift

        shifts[0] = start; // Starting cube is in this shift
        int i = 1; // Start from index 1
        Vector3 current = start; // Current cube we are shifting from
        while(!done)
        {
            if ((current[changingAxis] == 0 && direction == -1) || (current[changingAxis] == 2 && direction == 1)) // If we cannot continue moving in this direction
            {
                int temp = changingAxis; // Switch the changing axis to the other non-constant axis
                changingAxis = otherAxis;
                otherAxis = temp;
                if (current[changingAxis] == 0) // Configure the direction so the new changing axis can continue in that direction
                {
                    direction = 1;
                } else
                {
                    direction = -1;
                }
            } else
            { // Otherwise we move in the direction on the changing axis
                current[changingAxis] += direction;
                if (start == current) // If we are back at where we started, we are done
                {
                    done = true;
                }
                else
                { // Otherwise add the cube to the list of shifts
                    shifts[i] = current;
                    i++;
                }
            }
        }

        Color color = GetColor(shifts[7]); // Color of previous cube
        for (int j = 0;j < 8;j++)
        {
            Vector3 cur = shifts[j];
            Color temp = GetColor(cur); // Get color of current cube
            SetColor(cur, color); // Set color of current cube to previous cube
            color = temp; // Overwrite previous color with current color
        }
    }

    public void ShiftUp()
    {
        if (cursorFront)
        {
            // Constant axis - x
            // Initial changing axis - y
            // Move up
            Shift(0, 1, 2, 1);
        } else
        {
            // Constant axis - x
            // Initial changing axis - z
            // Move up
            Shift(0, 2, 1, 1);
        }
    }

    public void ShiftLeft()
    {
        if (cursorFront)
        {
            // Constant axis - y
            // Initial changing axis - x
            // Move left
            Shift(1, 0, 2, -1);
        }
        else
        {
            // Constant axis - z
            // Initial changing axis - x
            // Move left
            Shift(2, 0, 1, -1);
        }
    }

    public void ShiftDown()
    {
        if (cursorFront)
        {
            // Constant axis - x
            // Initial changing axis - y
            // Move down
            Shift(0, 1, 2, -1);
        }
        else
        {
            // Constant axis - x
            // Initial changing axis - y
            // Move down
            Shift(0, 2, 1, -1);
        }
    }

    public void ShiftRight()
    {
        if (cursorFront)
        {
            // Constant axis - y
            // Initial changing axis - x
            // Move right
            Shift(1, 0, 2, 1);
        }
        else
        {
            // Constant axis - z
            // Initial changing axis - x
            // Move right
            Shift(2, 0, 1, 1);
        }
    }
}
