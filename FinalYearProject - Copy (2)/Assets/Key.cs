using UnityEngine;

public class Key : InteractableObject
{
    public LevelThreeProgress LevelThreeProgress;
    // override the Interact method from the parent class InteractableObject
    protected override void Interact()
    {
        Debug.Log("Key Found");
        // call the KeyFound method from the LevelThreeProgress script
        LevelThreeProgress.KeyFound();
        Destroy(gameObject);
    }
}
