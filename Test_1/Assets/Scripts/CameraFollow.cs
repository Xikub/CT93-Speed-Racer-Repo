using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // Target is chosen in Inspector window

    public float smoothSpeed = 0.125f;  // Factor to smooth by
    public Vector3 offset;  // Offset is decided in X,Y,Z coords in Inspector window

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // This is where the camera is supposed to be, including offset
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);  // This smoothes using smoothed linear interpolation
        transform.position = smoothedPosition;  // Assign the new position

        transform.LookAt(target); // Ensures the camera always follows the same object, the player in this case
    }
}
