using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of rotation")]
    private float rotationSpeed = 270f;
    [SerializeField, Tooltip("Rotation origin of this object")]
    private Transform rotationOrigin;

    void Update()
    {
        RotateCamera();
    }

    /// <summary>
    /// Rotate the camera
    /// </summary>
    private void RotateCamera()
    {
        //Only rotate if pressing one of there buttons
        if (Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.RightArrow))
        {
            //get value from -1 to 1
            float rotate = -(Input.GetAxisRaw("Horizontal"));

            //rotate on the y axis by rotationSpeed
            rotationOrigin.Rotate(0f, rotate * rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
