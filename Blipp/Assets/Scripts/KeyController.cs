using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

    public CubeController cubecontroller;
    public CameraController cameracontroller;

    private KeyCode rotateKey; // Current key indicating direction we are rotating it

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // True if the user is holding down the Shift key
        bool shift = Input.GetKey(KeyCode.LeftShift);
        // True if the user is holding down the W, A, S, or D keys
        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(rotateKey)) // If the user lets go of shift or the held-down direction shift key, undo the rotation
        {
            cameracontroller.UndoRotate();
        }

        // Detect Shift + WASD for rotation
        if ((shift && Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.LeftShift) && w))
        {
            cameracontroller.RotateUp();
            rotateKey = KeyCode.W;
        }
        else if ((shift && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftShift) && a))
        {
            cameracontroller.RotateLeft();
            rotateKey = KeyCode.A;
        }
        else if ((shift && Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.LeftShift) && s))
        {
            cameracontroller.RotateDown();
            rotateKey = KeyCode.S;
        }
        else if ((shift && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.LeftShift) && d))
        {
            cameracontroller.RotateRight();
            rotateKey = KeyCode.D;
        }
        // WASD for cursor movement
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

        // Demo();
	}

    /*
    void Demo()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cubecontroller.cursor[0] = 0;
            cubecontroller.cursor[1] = 0;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = true;
            cubecontroller.RenderCursor();
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cubecontroller.cursor[0] = 1;
            cubecontroller.cursor[1] = 0;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = true;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cubecontroller.cursor[0] = 2;
            cubecontroller.cursor[1] = 0;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = true;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cubecontroller.cursor[0] = 0;
            cubecontroller.cursor[1] = 1;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = true;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cubecontroller.cursor[0] = 1;
            cubecontroller.cursor[1] = 1;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = true;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            cubecontroller.cursor[0] = 2;
            cubecontroller.cursor[1] = 1;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = true;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            cubecontroller.cursor[0] = 0;
            cubecontroller.cursor[1] = 2;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = false;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            cubecontroller.cursor[0] = 1;
            cubecontroller.cursor[1] = 2;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = false;
            cubecontroller.RenderCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            cubecontroller.cursor[0] = 2;
            cubecontroller.cursor[1] = 2;
            cubecontroller.cursor[2] = 0;
            cubecontroller.front = false;
            cubecontroller.RenderCursor();
        }
    }*/
}
