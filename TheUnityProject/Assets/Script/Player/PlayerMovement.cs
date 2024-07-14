using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 input;
    private Rigidbody rb;
    public float walkSpeed = 10;
    public float runSpeed = 18;
    private float speed = 10;
    private CharacterController controller;
    private Vector3 playerInput;
    // WALK RUN
    public bool isWalking = true;
    public bool isRunning = false;
    
    // JUMP
    public float jumpHeight = 2;
    public bool jumpEnable = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        /*
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning) speed = runSpeed;
        else speed = walkSpeed;
        
        // PLayer shuold move using WASD
        input = rb.velocity;
        
        input.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime * 250;
        input.z = Input.GetAxis("Vertical") * speed * Time.deltaTime * 250;

        rb.velocity = input;
        */

        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    }

    private void FixedUpdate()
    {
        if (playerInput.magnitude > 1)
        {
            playerInput = playerInput.normalized;
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            isWalking = false;
        }
        if (isRunning) speed = runSpeed;
        else speed = walkSpeed;

        controller.SimpleMove( speed * 250 * moveVector);
    }

    private void Jump()
    {
        Vector3 jumpVector = rb.velocity;
        jumpVector.y += jumpHeight;
        if (jumpEnable)
        {
            rb.velocity = jumpVector;
        }
    }
}
