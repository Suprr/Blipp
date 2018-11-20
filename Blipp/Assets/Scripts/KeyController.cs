using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

    public CubeController cubecontroller;

    private KeyCode rotateKey; // Current key indicating direction we are rotating it

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // True if the user is holding down the Shift key
        bool shift = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(rotateKey)) // If the user lets go of shift or the held-down direction shift key, undo the rotation
        {
            cubecontroller.UndoRotate();
        }

        if (shift)
        { // Detect Shift + WASD for rotation
            if (Input.GetKeyDown(KeyCode.W))
            {
                cubecontroller.RotateUp();
                rotateKey = KeyCode.W;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                cubecontroller.RotateLeft();
                rotateKey = KeyCode.A;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                cubecontroller.RotateDown();
                rotateKey = KeyCode.S;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                cubecontroller.RotateRight();
                rotateKey = KeyCode.D;
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
