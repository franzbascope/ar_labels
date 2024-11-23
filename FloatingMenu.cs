using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMenu : MonoBehaviour
{
    public Camera arCamera;  // Reference to AR Camera
    public float distanceFromCamera = 0.5f;  // How far the object is from the camera

    void Update()
    {
        if (arCamera != null)
        {
            // Position the GameObject in front of the AR Camera
            transform.position = arCamera.transform.position + arCamera.transform.forward * distanceFromCamera;

            // Optionally, make the object always face the camera
            //transform.LookAt(arCamera.transform);
        }
    }
}
