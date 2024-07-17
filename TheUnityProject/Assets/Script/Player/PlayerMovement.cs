using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider playerCollider;
    public GameObject cameraObject;
    public float fovChangeTime = 1f;
    private Camera camera;
    private float lastFOV;
    private float timer;
    private float fovTimer = 0;
    float fovTimer2 = 0;
    PlayerHealth healthScript;
    public float staminaRegen;
    
    // WALK RUN
    Vector3 moveVector;
    [Header("Walk Settings")]
    public float walkSpeed = 2000;
    public float walkFOV = 10;
    public bool isWalking = true;
    public bool canWalk = true;
    public bool PLAYSOUND_Walking;
    
    [Header("Run Settings")]
    public float runSpeed = 3600;
    public float runFOV = 10;
    public bool isRunning = false;
    public bool canRun = true;
    public bool PLAYSOUND_Running;
    public float runStaminaCost = 1;
    
    private float speed;
    
    // DASH
    //public string dashSettings = "----------------"; // For orginisation in editor
    [Header("Dash Settings")]
    public float dashSpeed = 20000;
    public float dashFOV = 10;
    public float dashtime = 2;
    public bool isDashing = false;
    public bool canDash = true;
    public bool PLAYSOUND_DASH;
    public float dashStaminaCost;
    
    private float dashTimer = 0;
    
    // JUMP
    Vector3 jumpVector;
    [Header("Jump Settings")]
    public float jumpHeight = 2;
    public bool canJump = true;
    public bool PLAYSOUND_Jump;
    public bool PLAYSOUND_JumpLand;
    public bool isGrounded = true;
    public bool isJumping = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        camera = cameraObject.GetComponent<Camera>();
        healthScript = GetComponent<PlayerHealth>();
        lastFOV = walkFOV;
    }

    void ResetSounds()
    {
        PLAYSOUND_Jump = false;
        PLAYSOUND_Running = false;
        PLAYSOUND_Walking = false;
        PLAYSOUND_JumpLand = false;
        PLAYSOUND_DASH = false;
    }
    void Update()
    {
        if (!isRunning && !isDashing && healthScript.playerStamina <= healthScript.playerMaxStamina)
        {
            healthScript.playerStamina += staminaRegen;
        }
        timer += Time.deltaTime;
        ResetSounds();
        if (speed == walkSpeed)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, walkFOV, Time.deltaTime*fovChangeTime);
            if (Math.Abs(walkFOV - camera.fieldOfView) < 1) camera.fieldOfView = walkFOV;
        } 
        else if (speed == runSpeed)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, runFOV, Time.deltaTime*fovChangeTime);
            if (Math.Abs(runFOV - camera.fieldOfView) < 1) camera.fieldOfView = runFOV;
        }
        else if (speed == dashSpeed)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, dashFOV, Time.deltaTime*fovChangeTime);
            if (Math.Abs(dashFOV - camera.fieldOfView) < 1) camera.fieldOfView = dashFOV;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing && healthScript.playerStamina >= dashStaminaCost)
        {
            dashTimer = timer;
            isDashing = true;
            PLAYSOUND_DASH = true;
            speed = dashSpeed;
            healthScript.playerStamina -= dashStaminaCost;
            
            fovTimer = (timer-fovTimer2)/fovChangeTime;
            if (fovTimer > 1)
            {
                fovTimer2 = timer;
            }
            lastFOV = camera.fieldOfView;
        }

        if (dashTimer + dashtime < timer)
        {
            isDashing = false;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && canRun && !isDashing && healthScript.playerStamina >= runStaminaCost)
        {
            isRunning = true;
            isWalking = false;
            PLAYSOUND_Running = true;
            speed = runSpeed;
            healthScript.playerStamina -= runStaminaCost;

            fovTimer = (timer-fovTimer2)/fovChangeTime;
            lastFOV = camera.fieldOfView;
            if (fovTimer > 1)
            {
                fovTimer2 = timer;
            }
            lastFOV = camera.fieldOfView;
        }
        else if (!isDashing)
        {
            PLAYSOUND_Walking = true;
            isWalking = true;
            isRunning = false;
            speed = walkSpeed;

            fovTimer = (timer - fovTimer2) / fovChangeTime;
            lastFOV = camera.fieldOfView;
            if (fovTimer > 1)
            {
                fovTimer2 = timer;
            }

            lastFOV = camera.fieldOfView;
        }

        RaycastHit hot;
        if (Physics.Raycast(transform.position,-transform.up, out hot,1.3f))
        {
            if (hot.collider.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            else
            {
                // If ray hits something else than ground
                isGrounded = false;
            }
        }
        else
        {
            // If ray hits nothing
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        // WALK RUN FORCE
        moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveVector.magnitude >= 1)
        {
            moveVector = moveVector.normalized;
        }
        
        
    }

    void FixedUpdate()
    {
        moveVector = transform.localRotation * moveVector;
        moveVector = speed * moveVector;
        moveVector.y = rb.velocity.y;
        moveVector.y -= transform.position.y * 0.01f; // Fix dash into enemy jump takes too long to fall down again (kinda fix only)
        rb.velocity = moveVector;
        moveVector = new Vector3();
        Debug.DrawRay(transform.position,moveVector,Color.red, Time.fixedDeltaTime);
    }

    /*
    void FOVGlide(float finalFOV) 
    {
        switch (fovDirection)
        {   
            case 0:
                if (camera.fieldOfView < finalFOV) fovDirection = 1;
                else fovDirection = 2;
                break;
            case 1:
                if (camera.fieldOfView < finalFOV)
                {
                    camera.fieldOfView += fovChangeSmoothness;
                }
                else
                {
                    camera.fieldOfView = finalFOV;
                    fovDirection = 3;
                }
                break;
            case 2:
                if (camera.fieldOfView > finalFOV)
                {
                    camera.fieldOfView -= fovChangeSmoothness;
                }
                else
                {
                    camera.fieldOfView = finalFOV;
                    fovDirection = 3;
                }
                break;
        }
    }
    */
    void Jump()
    {
        RaycastHit hit;
        if (isGrounded)
        {
            Debug.DrawRay(transform.position,-transform.up, Color.blue,3);
            // JUMP FORCE
            PLAYSOUND_Jump = true;
            isJumping = true;
            jumpVector = new Vector3(0, jumpHeight, 0);
            rb.velocity += jumpVector;
        }
    }

    /*IEnumerator PlayerJumpingLand()
    {
        if (transform.position.y < 1.2f && isGrounded)
        {
            isJumping = false;
        }

        yield return new WaitForSeconds(0);
        
        if (isJumping)
        {
            StartCoroutine("PlayerJumpingLand");
        }
        else
        {
            PLAYSOUND_JumpLand = true;
        }
    }
    */
}
