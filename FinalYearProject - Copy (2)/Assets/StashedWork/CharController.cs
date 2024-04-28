// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using static Models;
// public class CharController : MonoBehaviour
// {
//     private DefaultInput defaultInput;
//     public Vector2 input_Movement;
//     public Vector2 input_View;
//     
//     private Vector3 newCameraRotation;
//     
//     [Header("References")]
//     public Transform cameraHolder;
//     
//     [Header("Settings")]
//     public PlayerSettingsModel playerSettings;
//     private void Awake()
//     {
//         defaultInput = new DefaultInput();
//         
//         defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
//         defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
//         
//         defaultInput.Enable();
//         
//         newCameraRotation = cameraHolder.localRotation.eulerAngles;
//     }
//
//     // Start is called before the first frame update
//     private void Start()
//     {
//         
//     }
//
//     // Update is called once per frame
//     private void Update()
//     {
//         CalculateView();
//         CalculateMovement();
//     }
//     
//     private void CalculateMovement()
//     {
//         
//     }
//     
//     private void CalculateView()
//     {
//         newCameraRotation.x += playerSettings.ViewYSensitivity * input_View.y * Time.deltaTime;
//         cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
//     }
// }
