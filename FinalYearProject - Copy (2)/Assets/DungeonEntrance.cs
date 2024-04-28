using UnityEngine;

public class DungeonEntrance : InteractableObject
{
    public LevelTwoProgress LevelTwoProgress;
    
    // override the Interact method from the parent class InteractableObject
    protected override void Interact()
    {
        Debug.Log("Level Two Finishing");
        LevelTwoProgress.LevelTwoComplete();
    }
}
