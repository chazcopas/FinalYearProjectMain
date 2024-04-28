using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float minViewDistance = 25f;
    [SerializeField] Transform playerBody;
    
    public float mouseSensitivity = 100f;
    
    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //locks cursor on screen so that it doesnt move around when the player looks around
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // gets the mouse input and multiplies it by the mouse sensitivity and time and uses it to rotate the camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); // creates rotation so player rotates with the camera
    }
}
