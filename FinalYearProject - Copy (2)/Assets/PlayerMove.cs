using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] private float jumpPower = 5f;

    public bool fire;
    
    
    Vector2 moveInput;
    Rigidbody rb;
    bool isGrounded;
    
    private bool isTimeTravelling = false;
    public Material pastMaterial;
    public Material presentMaterial;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }
    
    // run method for player movement 
    void Run()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);
    }
    // jump method for player movement
    void Jump()
    {
        rb.AddForce(new Vector3(0,jumpPower,0), ForceMode.Impulse);
        isGrounded = false;
    }
    // checks if player is grounded
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    void OnFire(InputValue value)
    {
        fire = value.isPressed;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
