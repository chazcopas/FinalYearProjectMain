using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : InteractableObject
{
    public Player player;
    // override the Interact method from the parent class InteractableObject
    protected override void Interact()
    {
        Debug.Log("Health Pack Collected");
        player.HealthPackPickup();
        Destroy(gameObject);
    }
}
