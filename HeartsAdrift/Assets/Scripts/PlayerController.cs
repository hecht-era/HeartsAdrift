﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject otherObj;
    public GameObject boat;
    public Canvas canvas;

    public float speed = 5f;
    public float gravity = 20.0f;
    public float lookSpeed = 1.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    private bool _isTrigger;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        _isTrigger = false;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Horizontal") * lookSpeed;
            transform.eulerAngles = new Vector2(0, rotation.y);
        }

        if (Input.GetKeyDown(KeyCode.E) && _isTrigger)
        {
            boat.GetComponent<boat>().ResetState();
            _isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.transform.GetChild(2).gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        _isTrigger = true;
    }
}
