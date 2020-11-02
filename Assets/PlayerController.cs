using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject otherObj;
    [SerializeField] public GameObject boat;
    [SerializeField] public Canvas canvas;

    CharacterController characterController;
    Vector3 movement;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
            throw new UnityException("No Character Controller attached to capsule.");
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(transform.position.x * moveX/4, 0f, (transform.position.z) * -moveZ/4);

        moveVector.y -= 20.0f * Time.deltaTime;

        characterController.Move(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        canvas.transform.GetChild(4).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        canvas.transform.GetChild(4).gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            boat.GetComponent<boat>().ResetState();
        }
    }
}
