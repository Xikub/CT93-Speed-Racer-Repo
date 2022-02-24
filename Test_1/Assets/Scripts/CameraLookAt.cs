using UnityEngine;
using System.Collections;

public class CameraLookAt : MonoBehaviour
{
    public Transform target;    // Decide target from Inspector window
    
    void Update ()
    {
        transform.LookAt(target);   // Makes object face target every frame
    }
}