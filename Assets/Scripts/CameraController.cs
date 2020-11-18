using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    public void Update() // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        if(StateManager.Instance.GetState() != GameState.READING)
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
            transform.eulerAngles = new Vector2(rotation.x, rotation.y) * lookSpeed;
            Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, rotation.y * lookSpeed, 0);
        }
    }
}
