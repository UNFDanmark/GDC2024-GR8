using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    // WALK RUN
    public float walkSpeed = 10;
    public bool isWalking = true;
    
    public float runSpeed = 18;
    public bool isRunning = false;
    
    private Vector3 playerInput;
    private float speed;
    
    // JUMP
    public float jumpHeight = 2;
    public bool jumpEnable = true;
    
    private Vector3 jumpVector;
    
    void Start()
    {
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
        jumpVector = new Vector3(playerInput.x, jumpHeight, playerInput.z);
        jumpVector = new Vector3(0, jumpHeight, 0);

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO Wait for jump to finish b4 starting new jump
            Jump();
        }
        
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
        jumpVector.y += jumpHeight;
        if (jumpEnable)
        {
            // TODO Make player move with charactermover
            controller.SimpleMove(jumpVector);
        }
    }
}
