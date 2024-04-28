using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //player parent object
    public GameObject player;
    
    //player stats
    //health
    public float maxHealth;
    private float currentHealth;
    public float healthRestoreRate;
    public int numberOfHealthRestores;
    public float restoreHealthBy;
    
    public float timeBeforeRestore;
    //stamina
    public float maxStamina;
    private float currentStamina;
    public float staminaRestoreRate;
    public float restoreStaminaBy;
    
    //Number of kills (for upgrades)
    public int numberOfKills;
    public int numberOfKillsNeeded;

    
    //time travel script
    private TimeTravelV2 timeTravelV2;
    
    // player Weapons
    public GunV2 gun;
    public SwordsV2 sword;
    
    // audio sources
    public AudioSource playerHurt;
    public AudioSource playerHurtBlocked;
    public AudioSource playerHeal;
    
    // player UI
    public Slider HealthSlider;
    public TMP_Text healthRestoreText;
    public Slider StaminaSlider;
    public TMP_Text bulletsLeftText;
    public GameObject upgradePanel;
    
    private void Start()
    {
        // set players stats
        timeTravelV2 = player.GetComponent<TimeTravelV2>();
        numberOfKills = 0;
        currentHealth = maxHealth;
        HealthSlider.value = CalculateHealth();
        currentStamina = maxStamina;
        healthRestoreText.text = "(H) Health Packs: " + numberOfHealthRestores;
        // StaminaSlider.value = CalculateStamina();
    }
    
    void Update()
    {
        // Debug.Log("Player health: " + currentHealth);
        // keeps track of player health and stamina and updates the UI ensuring it remains accurate
        HealthSlider.value = CalculateHealth();
        StaminaSlider.value = CalculateStamina();
        // shows the number of bullets left in the gun if the player is in the present
        if (timeTravelV2.InPresentQuery())
        {
            bulletsLeftText.text = gun.bulletsLeft + " / " + gun.magazineSize;
        }
        else
        {
            bulletsLeftText.text = "";
        }
        
        if (currentHealth <= 0.0)
        {
            Die();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        // activate upgrade panel and increase number of kills needed to open it
        if (numberOfKills == numberOfKillsNeeded)
        {
            upgradePanel.GetComponent<UpgradeStats>().OpenPanel();
            numberOfKillsNeeded++;
            numberOfKills = 0;
        }
        
        // play sound of heartbeat when health is below 20% of max health
        if (currentHealth < maxHealth / 5)
        {
            // start playing heartbeat sound
            
        }
        if (currentHealth > maxHealth / 5)
        {
            // stop playing heartbeat sound
        }
 
        
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        
        if (currentStamina <= 0)
        {
            currentStamina = 0;
        }
        // restore health and stamina when player uses health pack by pressing H
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseHealthRestore();
        }
    }
    
    // method for using health pack to restore health and stamina by a set amount if the player has any health packs
    public void UseHealthRestore()
    {
        if (numberOfHealthRestores > 0)
        {
            playerHeal.Play();
            currentHealth += restoreHealthBy;
            currentStamina += restoreStaminaBy;
            numberOfHealthRestores--;
            healthRestoreText.text = "(H) Health Packs: " + numberOfHealthRestores;
        }
    }
    
    // method for picking up health packs
    public void HealthPackPickup()
    {
        numberOfHealthRestores++;
        healthRestoreText.text = "(H) Health Packs: " + numberOfHealthRestores;
    }
    
    // method for taking damage
    public void TakeDamage(float damage)
    {
        // if the player is blocking then reduce the damage by half and play the blocked sound
        if (sword.isBlocking)
        {
            playerHurtBlocked.Play();
            Debug.Log("Player is blocking so damage reduced by half!");
            damage = damage / 2;
        }
        else
        {
            // play the player hurt sound
            playerHurt.Play();
        }
        // reduce the players health by the damage amount
        currentHealth -= damage;
        Debug.Log("Player health: " + currentHealth);
        // kill player if health is 0 or less
        if (currentHealth <= 0.0)
        {
            Die();
        }
    }
    
    //reduce stamina
    public void reduceStamina(float stamina)
    {
        Debug.Log("Reducing stamina by: " + stamina);
        currentStamina -= stamina;
    }
    
    public int returnStamina()
    {
        return (int)currentStamina;
    }
    // upgrade functions
    public void increaseMaxStamina(float stamina)
    {
        maxStamina += stamina;
        currentStamina = maxStamina;
    }
    
    public void IncreaseMaxHealth(float health)
    {
        maxHealth += health;
        currentHealth = maxHealth;
    }
    
    public void increaseRestoreBy(float increase)
    {
        restoreHealthBy += increase;
        restoreStaminaBy += increase;
    }
    
    // method for player death
    public void Die()
    {
        Destroy(player);
        Destroy(gameObject);
        // for now end the game
        Application.Quit();
    }
    
    // method for increasing the number of kills
    public void IncreaseNumberOfKills()
    {
        numberOfKills++;
    }
    
    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
    
    private float CalculateStamina()
    {
        return currentStamina / maxStamina;
    }
}
