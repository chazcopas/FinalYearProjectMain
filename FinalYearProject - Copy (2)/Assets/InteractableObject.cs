using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string promptText;
    // abstract class used as a base for different interactable objects
    
    public void InteractBase()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // accessed and overridden by child classes
    }
}
