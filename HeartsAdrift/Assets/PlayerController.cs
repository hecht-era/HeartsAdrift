using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject otherObj;
    [SerializeField] public GameObject boat;
    [SerializeField] public Canvas canvas;
    [SerializeField] public float playerSpeed;

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

        // Calculate the Direction to Move based on the tranform of the Player
        Vector3 moveDirectionForward = transform.forward * moveZ;
        Vector3 moveDirectionSide = transform.right * moveX;

        //find the direction
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        //find the distance
        Vector3 distance = direction * playerSpeed * Time.deltaTime;

        // Apply Movement to Player
        characterController.Move(distance);
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
