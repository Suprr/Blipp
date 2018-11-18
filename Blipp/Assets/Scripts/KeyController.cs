using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

    public CubeController cubecontroller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // True if the user is holding down the Shift key
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (shift)
        { // Detect Shift + WASD for rotation
            if (Input.GetKeyDown(KeyCode.W))
            {
                cubecontroller.RotateUp();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                cubecontroller.RotateLeft();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                cubecontroller.RotateDown();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                cubecontroller.RotateRight();
            }
        } // WASD for cursor movement
        else if (Input.GetKeyDown(KeyCode.W))
        {
            cubecontroller.CursorUp();
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            cubecontroller.CursorLeft();
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            cubecontroller.CursorDown();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            cubecontroller.CursorRight();
        } // Up, Left, Down, Right for shifting
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cubecontroller.ShiftUp();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cubecontroller.ShiftLeft();
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cubecontroller.ShiftDown();
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cubecontroller.ShiftRight();
        }
	}
}
