using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    public float turnSpeed = 1000f;
    public float moveSpeed = 1000f;

    private Rigidbody rBody;
    [SerializeField] public GameObject compass;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 m_EulerAngleVelocity = new Vector3(0f, h * turnSpeed * Time.deltaTime, 0f);

        Vector3 compassRotation = compass.transform.eulerAngles;
        compassRotation.z = -transform.eulerAngles.y;
        compass.transform.eulerAngles = compassRotation;

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rBody.MoveRotation(rBody.rotation * deltaRotation);
        
        rBody.AddForce(transform.forward * v * moveSpeed * Time.deltaTime);
    }
}
