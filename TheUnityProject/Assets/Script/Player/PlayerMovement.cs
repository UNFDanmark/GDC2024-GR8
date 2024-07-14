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

    public bool PLAYSOUND_Running;
    public bool PLAYSOUND_Walking;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (isRunning)
            {
                PLAYSOUND_Running = isRunning;
            }
            else
            {
                PLAYSOUND_Walking = isWalking;
            }
        }
        else
        {
            PLAYSOUND_Walking = isWalking;
            PLAYSOUND_Running = isRunning;
        }
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        jumpVector = new Vector3(playerInput.x, jumpHeight, playerInput.z);
        //jumpVector = new Vector3(0, jumpHeight, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO Wait for jump to finish b4 starting new jump
            Jump();
            jumpEnable = false;
        }
        Debug.Log("Run: " + PLAYSOUND_Running);
        Debug.Log("Walk: " + PLAYSOUND_Walking);

    }

    private void FixedUpdate()
    {
        if (playerInput.magnitude > 1)
        {
            playerInput = playerInput.normalized;
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            isRunning = true;
            isWalking = false;
            PLAYSOUND_Running = true;
            PLAYSOUND_Walking = false;
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isRunning = false;
            isWalking = true;
            PLAYSOUND_Running = false;
            PLAYSOUND_Walking = true;
        }
        else
        {
            isRunning = false;
            isWalking = false;
            PLAYSOUND_Running = false;
            PLAYSOUND_Walking = false;
        }
        if (isRunning) speed = runSpeed;
        else speed = walkSpeed;

        controller.SimpleMove( speed * 250 * moveVector);
        
    }

    private void Jump()
    {
        if (jumpEnable)
        {
            // TODO Fix jump (teleporting rn)
            Debug.Log("JUMP");
            Vector3 jump = transform.TransformDirection(jumpVector);
            controller.Move(jump);
        }
    }
}
