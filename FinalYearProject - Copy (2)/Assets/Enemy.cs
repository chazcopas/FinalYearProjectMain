using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // player
    public Player player;
    // health
    public float maxHealth;
    private float currentHealth;
    // ui for health bar
    public GameObject healthBarUI;
    public Slider slider;
    
    private void Start()
    {
        // set health as defined in the inspector
        currentHealth = maxHealth;
        slider.value = CalculateHealth();
    }
    
    void Update()
    {
        // update health bar every frame so UI is accurate
        slider.value = CalculateHealth();
        if (currentHealth < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        // destroy enemy if health is 0 or less
        if (currentHealth <= 0.0)
        {
            Destroy(gameObject);
        }
        // make sure health doesn't go above max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    // take damage, called when player or enemy attacks this object
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0)
        {
            player.IncreaseNumberOfKills();
            Die();
        }
    }
    
    // destroy enemy
    void Die()
    {
        Destroy(gameObject);
    }
    
    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
}
