using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    private float interactDistance = 3f;
    // layer of interactable objects
    [SerializeField]
    private LayerMask interactableLayer;
    
    void Update()
    {
        // Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        // ray from the center of the camera in the direction the camera is facing
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
        RaycastHit hit;
        // checks if ray hits an object with an interactable layer
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // checks if layer is interactable
            if (hit.collider.GetComponent<InteractableObject>() != null)
            {
                // Debug.Log(hit.collider.GetComponent<InteractableObject>().promptText);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<InteractableObject>().InteractBase();
                }
            }
        }
    }
}
