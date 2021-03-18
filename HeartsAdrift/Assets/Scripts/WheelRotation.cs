using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour
{
    // rotation speed in degrees per second.
    public float rotationSpeed = 5f;

    void Update()
    {
        // Rotate around X Axis
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
