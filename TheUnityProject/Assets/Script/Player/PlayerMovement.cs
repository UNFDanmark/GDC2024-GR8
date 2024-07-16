using System;
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
    
    // WALK RUN
    Vector3 moveVector;
    public string walkSettings = "----------------"; // For orginisation in editor
    public float walkSpeed = 2000;
    public float walkFOV = 10;
    public bool isWalking = true;
    public bool canWalk = true;
    public bool PLAYSOUND_Walking;
    
    public string runSettings = "----------------"; // For orginisation in editor
    public float runSpeed = 3600;
    public float runFOV = 10;
    public bool isRunning = false;
    public bool canRun = true;
    public bool PLAYSOUND_Running;
    
    private float speed;
    
    // DASH
    public string dashSettings = "----------------"; // For orginisation in editor
    public float dashSpeed = 20000;
    public float dashFOV = 10;
    public float dashtime = 2;
    public bool isDashing = false;
    public bool canDash = true;
    public bool PLAYSOUND_DASH;
    
    private float dashTimer = 0;
    
    // JUMP
    Vector3 jumpVector;
    public string jumpSettings = "----------------"; // For orginisation in editor
    public float jumpHeight = 2;
    public bool canJump = true;
    public bool PLAYSOUND_Jump;
    public bool PLAYSOUND_JumpLand;
    public bool isGrounded = true;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        camera = cameraObject.GetComponent<Camera>();
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
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing)
        {
            dashTimer = timer;
            isDashing = true;
            PLAYSOUND_DASH = true;
            speed = dashSpeed;
            
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
        
        if (Input.GetKey(KeyCode.LeftShift) && canRun && !isDashing)
        {
            isRunning = true;
            isWalking = false;
            PLAYSOUND_Running = true;
            speed = runSpeed;

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
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveVector.magnitude >= 1)
        {
            moveVector = moveVector.normalized;
        }
        
        
    }

    void FixedUpdate()
    {
        moveVector = transform.TransformDirection(moveVector);
        moveVector = speed * moveVector;
        moveVector.y = rb.velocity.y;

        moveVector.y -= transform.position.y * 0.01f; // Fix dash into enemy jump takes too long to fall down again (kinda fix only)
        
        
        rb.velocity = moveVector;

        
        
        Debug.DrawRay(transform.position,moveVector,Color.red,0.5f);
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
            jumpVector = new Vector3(0, jumpHeight, 0);
            rb.velocity += jumpVector;
        
            
        }
    }
}
