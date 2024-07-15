using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    Rigidbody rb;
    Collider playerCollider;
    float timer;
    // WALK RUN
    Vector3 moveVector;
    public float walkSpeed = 2000;
    public bool isWalking = true;
    public bool canWalk = true;
    public bool PLAYSOUND_Walking;
    
    public float runSpeed = 3600;
    public bool isRunning = false;
    public bool canRun = true;
    public bool PLAYSOUND_Running;
    
    private float speed;
    // DASH
    public float dashSpeed = 20000;
    public float dashtime = 2;
    public bool isDashing = false;
    public bool canDash = true;
    public bool PLAYSOUND_DASH;
    private float dashTimer = 0;
    // JUMP
    Vector3 jumpVector;
    public float jumpHeight = 2;
    public bool canJump = true;
    public bool PLAYSOUND_Jump;
    public bool PLAYSOUND_JumpLand;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
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
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing)
        {
            dashTimer = timer;
            isDashing = true;
            PLAYSOUND_DASH = true;
            speed = dashSpeed;
        }

        if (dashTimer + dashtime < timer)
        {
            isDashing = false;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && canRun && !isDashing)
        {
            isRunning = true;
            PLAYSOUND_Running = true;
            speed = runSpeed;
        }
        else if (!isDashing)
        {
            PLAYSOUND_Walking = true;
            speed = walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        // WALK RUN FORCE
        moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveVector = transform.TransformDirection(moveVector);
        moveVector = speed * Time.deltaTime * moveVector;
        moveVector.y = rb.velocity.y;
        if (canWalk) rb.velocity = moveVector;
        
        Debug.DrawRay(transform.position,moveVector,Color.red,0.5f);
        
    }
    void Jump()
    {
        RaycastHit hit;
        if (canJump && Physics.Raycast(transform.position,-transform.up, out hit,1.3f))
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawRay(transform.position,-transform.up, Color.blue,3);
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                // JUMP FORCE
                jumpVector = new Vector3(0, jumpHeight, 0);
                rb.velocity += jumpVector;

            }
        }
    }
}
