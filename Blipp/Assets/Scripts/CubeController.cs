using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject cube;
    public float rotateSpeed;
    public GameObject[] initCubes;

    private GameObject[][][] cubes; // 1st dimension - x, 2nd dimension - y, 3rd dimension - z
    public int[] cursor; // Current cursor position, TEMPORARILY PUBLIC FOR DEMO
    public bool front; // Current cursor face, TEMPORARILY PUBLIC FOR DEMO
    private bool rotating; // True if cube is rotating, false otherwise
    private float rotateProgress; // Progress in degrees of cube rotation so far
    private Vector3 rotateDir; // Direction to apply rotation in
    private bool undoRotate; // True if undo rotation is needed, also controls if shifting is allowed

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

        undoRotate = false;

        // Randomize the colors every second
        // InvokeRepeating("RandomizeColors", 0.0f, 1.0f);
        RandomizeColors();

        // Change the border of one cube to be white to represent the selected or "cursor" cube
        cursor = new int[3];
        cursor[0] = 2;
        cursor[1] = 2;
        cursor[2] = 0;
        front = false;

        RenderCursor();
    }
	
	// Update is called once per frame
	void Update () {
        PerformRotation();
	}

    // Renders the cursor by setting its border to white and all other borders to black
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
                    if (i == cursor[0] && j == cursor[1] && k == cursor[2])
                    {
                        colorMat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    } else
                    {
                        colorMat.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                    }
                }
            }
        }
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
                rotateProgress = 90f;
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
        if (!rotating && !undoRotate) // Only allow the cube to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(1, 0, 0);
            undoRotate = true;
        }
    }

    public void RotateLeft()
    {
        if (!rotating && !undoRotate) // Only allow the cube to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(0, 1, 0);
            undoRotate = true;
        }
    }

    public void RotateDown()
    {
        if (!rotating && !undoRotate) // Only allow the cube to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(-1, 0, 0);
            undoRotate = true;
        }
    }

    public void RotateRight()
    {
        if (!rotating && !undoRotate) // Only allow the cube to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(0, -1, 0);
            undoRotate = true;
        }
    }

    // Undo a rotation
    public void UndoRotate()
    {
        if (undoRotate) // Only allow a rotation to be undone if a rotation needs to be undone
        {
            rotateProgress = 90 - rotateProgress; // Invert the progress and rotation direction
            rotateDir *= -1;
            rotating = true; // Allow the cube to start rotating again, although with undoRotate set to false this time
            undoRotate = false;
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

    // Set the color of a cube at the given Vector coordinates
    void SetColor(Vector3 coords, Color color)
    {
        GameObject cube = cubes[(int)Mathf.Round(coords[0])][(int)Mathf.Round(coords[1])][(int)Mathf.Round(coords[2])]; // Get cube at Vector coordinates by rounding
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>(); // Get mesh renderer to get color material
        Material colorMat = renderer.materials[1];
        colorMat.color = color; // Set color
    }

    // Get the color of a cube at the given Vector coordinates
    Color GetColor(Vector3 coords)
    {
        GameObject cube = cubes[(int) Mathf.Round(coords[0])][(int)Mathf.Round(coords[1])][(int)Mathf.Round(coords[2])]; // Get cube at Vector coordinates by rounding
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>(); // Get mesh renderer to get color material
        Material colorMat = renderer.materials[1];
        return colorMat.color; // Return color
    }

    // Shift the cubes in a certain direction around the cube
    // x, y, z - starting coordinates
    // Note that an axis in this algorithm is 0 - x, 1 - y, 2 - z
    // constantAxis - axis that is not changed during shifting, so the shift is completely independent of this axis
    // startChangingAxis - axis that is initially changed (so shifted towards) in the start of the algorithm
    // startOtherAxis - other axis that is neither always constant nor the starting change axis, so it is temporarily constant
    // initDirection - initial direction that the changing axis changes by, so either +1 or -1
    void Shift(int x, int y, int z, int constantAxis, int startChangingAxis, int startOtherAxis, int initDirection)
    {
        if (undoRotate) // If shifting is not allowed, do nothing
        {
            return;
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
        if (front)
        {
            // Constant axis - x
            // Initial changing axis - y
            // Move up
            Shift(cursor[0], cursor[1], cursor[2], 0, 1, 2, 1);
        } else
        {
            // Constant axis - x
            // Initial changing axis - z
            // Move up
            Shift(cursor[0], cursor[1], cursor[2], 0, 2, 1, 1);
        }
    }

    public void ShiftLeft()
    {
        if (front)
        {
            // Constant axis - y
            // Initial changing axis - x
            // Move left
            Shift(cursor[0], cursor[1], cursor[2], 1, 0, 2, -1);
        }
        else
        {
            // Constant axis - z
            // Initial changing axis - x
            // Move left
            Shift(cursor[0], cursor[1], cursor[2], 2, 0, 1, -1);
        }
    }

    public void ShiftDown()
    {
        if (front)
        {
            // Constant axis - x
            // Initial changing axis - y
            // Move down
            Shift(cursor[0], cursor[1], cursor[2], 0, 1, 2, -1);
        }
        else
        {
            // Constant axis - x
            // Initial changing axis - y
            // Move down
            Shift(cursor[0], cursor[1], cursor[2], 0, 2, 1, -1);
        }
    }

    public void ShiftRight()
    {
        if (front)
        {
            // Constant axis - y
            // Initial changing axis - x
            // Move right
            Shift(cursor[0], cursor[1], cursor[2], 1, 0, 2, 1);
        }
        else
        {
            // Constant axis - z
            // Initial changing axis - x
            // Move right
            Shift(cursor[0], cursor[1], cursor[2], 2, 0, 1, 1);
        }
    }
}
