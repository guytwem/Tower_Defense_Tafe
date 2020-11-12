using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Moving Variables

    [SerializeField, Tooltip("Rotation origin of this object")]
    private Transform rotationOrigin;

    [Header("Rotation")]
    [SerializeField, Tooltip("Speed of rotation")]
    private float rotationSpeed = 180f;

    [Header("Scale")]
    [SerializeField, Tooltip("Speed moveing in and out")]
    private float scaleFactor = 1f;
    [SerializeField, Tooltip("Inner limit")]
    private float scaleInLimit = 0.3f;
    [SerializeField, Tooltip("Outer limit")]
    private float scaleOutLimit = 1.2f;

    [Header("UpDownMove")]
    [SerializeField, Tooltip("Speed moveing in and out")]
    private float upDownSpeed = 4f;
    [SerializeField, Tooltip("Upper limit")]
    private float lowerLimit = 2.3f;
    [SerializeField, Tooltip("Lower limit")]
    private float upperLimit = 4.5f;

    #endregion

    void Update()
    {
        RotateCamera();
        MoveCameraInOut();
        MoveCameraUpDown();
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

    private void MoveCameraInOut()
    {
        if (Input.GetKey(KeyCode.W)
             || Input.GetKey(KeyCode.S)
             || Input.GetKey(KeyCode.UpArrow)
             || Input.GetKey(KeyCode.DownArrow))
        {
            //get value from -1 to 1
            float scale = -(Input.GetAxisRaw("Vertical"));

            Vector3 currentScale = rotationOrigin.localScale;
            currentScale.x += scale * scaleFactor * Time.deltaTime;
            currentScale.y += scale * scaleFactor * Time.deltaTime;
            currentScale.z += scale * scaleFactor * Time.deltaTime;

            currentScale.x = Mathf.Clamp(currentScale.x, scaleInLimit, scaleOutLimit);
            currentScale.y = Mathf.Clamp(currentScale.y, scaleInLimit, scaleOutLimit);
            currentScale.z = Mathf.Clamp(currentScale.z, scaleInLimit, scaleOutLimit);

            rotationOrigin.localScale = currentScale;
        }
    }

    private void MoveCameraUpDown()
    {
        int upDownValue = MadeUpInputThing();

        Vector3 originPosition = rotationOrigin.position;
        originPosition.y += upDownValue * upDownSpeed * Time.deltaTime;
        originPosition.y = Mathf.Clamp(originPosition.y, lowerLimit, upperLimit);

        rotationOrigin.position = originPosition;
    }

    /// <summary>
    /// returns -1, 0, 1 for given input values
    /// </summary>
    /// <returns></returns>
    private int MadeUpInputThing()
    {
        if (Input.GetKey(KeyCode.E))
        {
            return 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

}
