using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputController : MonoBehaviour
{
    private DefaultInputActions controls;
    private Vector2 moveInput;
    private Vector2 lookInput;

    public float moveSpeed;
    public float sensitivity;
    public float jumpForce;
    public bool isGrounded;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new DefaultInputActions();
            controls.Player.Enable();
        }
    }
}
