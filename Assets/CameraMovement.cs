using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Camera movement speed
    public float cameraSpeed;

    private void FixedUpdate()
    {
        // Move camera up by camera speed
        transform.position += Vector3.up * cameraSpeed;
    }
}
