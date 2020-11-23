using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpacePointerCorrectDir : MonoBehaviour
{
    [SerializeField, Tooltip("Camera position of choice")] private Transform camPosition;

    private void Start()
    {
        // make sure at beginning pointer object is off
        gameObject.SetActive(false);
    }

    void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        // look at camera
        transform.LookAt(camPosition.position);
        Vector3 pointRotation = transform.eulerAngles;
        // rotate y by 180 to make pointer face camera
        pointRotation.y = pointRotation.y - 180f;
        pointRotation.x = 0;
        transform.eulerAngles = pointRotation;
    }
}
