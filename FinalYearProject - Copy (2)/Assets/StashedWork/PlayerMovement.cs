using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed;
    
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Detection")] 
    public float playerHeight;   
    public LayerMask groundMask;
    bool isGrounded;
    
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    
    Vector3 moveDirection;
    
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }
    
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position +  new Vector3(0, 0.05f, 0), Vector3.down, playerHeight * 0.5f +0.2f, groundMask);
        MyInput();
        SpeedControl();
        Debug.Log("isGrounded: " + isGrounded);
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //calculate direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if(isGrounded)
            rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * (moveSpeed * airMultiplier * 10f), ForceMode.Force);
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            flatVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
    private void ResetJump()
    {
        readyToJump = true;
    }
}
