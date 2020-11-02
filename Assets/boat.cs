using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class boat : MonoBehaviour
{
    private enum boatState
    {
        SAILING,
        DOCKING,
        UNDOCKING,
        DOCKED,
        WALKING
    }

    public float turnSpeed = 1000f;
    public float moveSpeed = 1000f;
    private boatState state;
    private GameObject otherObj;

    private Rigidbody rBody;
    [SerializeField] public GameObject compass;
    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject player;
    [SerializeField] public Camera cam;
    [SerializeField] public Transform cameraPos;
    [SerializeField] public Transform playerPos;
    [SerializeField] public Transform crow;
    private bool dockProcedure = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        state = boatState.SAILING;
    }

    void FixedUpdate()
    {
        if(state != boatState.DOCKED && state != boatState.WALKING)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 m_EulerAngleVelocity = new Vector3(0f, h * turnSpeed * Time.deltaTime, 0f);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);

            if (state == boatState.SAILING)
            {
                rBody.MoveRotation(rBody.rotation * deltaRotation);
                rBody.AddForce(transform.forward * v * moveSpeed * Time.deltaTime);
            }

            Quaternion compassRotation = deltaRotation;
            compassRotation.x = compass.transform.rotation.x;
            compassRotation.z = compass.transform.rotation.z;
            compassRotation.y += 180;
            compass.transform.rotation = compassRotation;
        }
    }
    private void Update()
    {
        Debug.Log(state);
        if (state == boatState.DOCKING)
        {
            transform.position = Vector3.MoveTowards(transform.position, otherObj.transform.position, Time.deltaTime * 2);
            transform.forward = Vector3.RotateTowards(transform.forward, otherObj.transform.right * -1, Time.deltaTime / 2, Time.deltaTime / 2);
            if(transform.position == otherObj.transform.position && transform.forward == otherObj.transform.right * -1)
            {
                state = boatState.DOCKED;
            }
        }
        if (state == boatState.UNDOCKING)
        {
            transform.position = Vector3.MoveTowards(transform.position, otherObj.transform.GetChild(0).transform.position, Time.deltaTime * 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Docking")
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true);
        }
        if(other.tag == "Exit" && state == boatState.UNDOCKING)
        {
            state = boatState.SAILING;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Docking")
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Docking" && state != boatState.WALKING)
        {
            if (Input.GetKeyDown(KeyCode.E) && state == boatState.SAILING)
            {
                canvas.transform.GetChild(2).gameObject.SetActive(false);
                canvas.transform.GetChild(3).gameObject.SetActive(true);
                otherObj = other.gameObject;
                state = boatState.DOCKING;
            }
            if (Input.GetKeyDown(KeyCode.R) && state == boatState.DOCKED)
            {
                canvas.transform.GetChild(3).gameObject.SetActive(false);
                canvas.transform.GetChild(2).gameObject.SetActive(true);
                otherObj = other.gameObject;
                state = boatState.UNDOCKING;
            }
            if (Input.GetKeyDown(KeyCode.E) && state == boatState.DOCKED)
            {
                canvas.transform.GetChild(3).gameObject.SetActive(false);
                canvas.transform.GetChild(2).gameObject.SetActive(false);
                cam.transform.position = crow.position;
                player.transform.position = otherObj.transform.parent.GetChild(12).position;
                player.transform.rotation = otherObj.transform.parent.GetChild(12).rotation;
                player.gameObject.SetActive(true);
                state = boatState.WALKING;
                player.transform.SetParent(null);
            }
        }
    }

    public void ResetState()
    {
        cam.transform.position = cameraPos.position;
        player.transform.position = playerPos.position;
        player.transform.rotation = playerPos.rotation;
        player.transform.SetParent(gameObject.transform);
        player.SetActive(false);
        state = boatState.DOCKED;
        canvas.transform.GetChild(3).gameObject.SetActive(true);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(4).gameObject.SetActive(false);
    }
}
