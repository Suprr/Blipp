using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float lookAtX;
    public float lookAtY;
    public float lookAtZ;
    public float rotateSpeed;

    private bool rotating; // True if camera is rotating, false otherwise
    private float rotateProgress; // Progress in degrees of camera rotation so far
    private Vector3 rotateDir; // Direction to apply rotation in
    private bool undoRotate; // True if undo rotation is needed, also controls if shifting is allowed

    // Use this for initialization
    void Start () {
        gameObject.transform.forward = new Vector3(lookAtX - gameObject.transform.position.x, lookAtY - gameObject.transform.position.y, lookAtZ - gameObject.transform.position.z);
        rotating = false;
        undoRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        PerformRotation();
    }

    // Perform the rotation animation on the camera if it is necessary
    void PerformRotation()
    {
        if (rotating) // Check if the camera is rotating
        {
            float newRotate = rotateSpeed * Time.deltaTime; // Generate the necessary rotation to perform
            rotateProgress += newRotate;
            if (rotateProgress >= 90f) // If we have rotated past 90 degrees, stop rotating
            {
                // Set the rotation back to exactly 90 degrees
                gameObject.transform.RotateAround(new Vector3(lookAtX, lookAtY, lookAtZ), rotateDir, newRotate + 90f - rotateProgress);
                rotateProgress = 90f;
                rotating = false;
            }
            else
            { // Otherwise apply the rotation
                gameObject.transform.RotateAround(new Vector3(lookAtX, lookAtY, lookAtZ), rotateDir, newRotate);
            }
        }
    }

    public void RotateUp()
    {
        if (!rotating && !undoRotate) // Only allow the camera to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(1, 0, 0);
            undoRotate = true;
        }
    }

    public void RotateLeft()
    {
        if (!rotating && !undoRotate) // Only allow the camera to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(0, 1, 0);
            undoRotate = true;
        }
    }

    public void RotateDown()
    {
        if (!rotating && !undoRotate) // Only allow the camera to rotate if it is not already rotating and we do not need to undo a rotation
        {
            rotating = true;
            rotateProgress = 0;
            rotateDir = new Vector3(-1, 0, 0);
            undoRotate = true;
        }
    }

    public void RotateRight()
    {
        if (!rotating && !undoRotate) // Only allow the camera to rotate if it is not already rotating and we do not need to undo a rotation
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
            rotating = true; // Allow the camera to start rotating again, although with undoRotate set to false this time
            undoRotate = false;
        }
    }

    public bool GetRotating()
    {
        return rotating;
    }

    public bool GetUndoRotate()
    {
        return undoRotate;
    }
}
