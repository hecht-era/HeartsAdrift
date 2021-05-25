using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour
{
    // rotation speed in degrees per second.
    public float rotationSpeedX = 5f;
    public float rotationSpeedZ = 0f;

    void Update()
    {
        // Rotate around X Axis
        transform.Rotate(Vector3.right * rotationSpeedX * Time.deltaTime);
        // Rotate around Z Axis
        transform.Rotate(Vector3.forward * rotationSpeedZ * Time.deltaTime);
    }
}
