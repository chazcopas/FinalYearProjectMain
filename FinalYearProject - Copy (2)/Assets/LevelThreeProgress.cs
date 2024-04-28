using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeProgress : MonoBehaviour
{
    private bool levelThreeComplete;
    private int keyCount;
    public Player player;
    public TimeTravelV2 timeTravelV2;
    
    private void Awake()
    {
        //retrieve the playerprefs values and set them to appropriate variables
        
        //set Time Travel Cooldown
        timeTravelV2.timeTravelCooldown = PlayerPrefs.GetFloat("Time Travel Cooldown");
        //set player max health
        player.maxHealth = PlayerPrefs.GetFloat("Player Max Health");
        //set player max stamina
        player.maxStamina = PlayerPrefs.GetFloat("Player Max Stamina");
        //set player health restore by
        player.restoreHealthBy = PlayerPrefs.GetFloat("Player Restore Health By");
        //set player stamina restore by
        player.restoreStaminaBy = PlayerPrefs.GetFloat("Player Restore Stamina By");
        //set gun damage
        player.gun.damage = PlayerPrefs.GetFloat("Gun Damage");
        //set reload time
        player.gun.reloadTime = PlayerPrefs.GetFloat("Gun Reload Time");
        //set gun spread
        player.gun.spread = PlayerPrefs.GetFloat("Gun Spread");
        //set sword damage
        player.sword.damage = PlayerPrefs.GetFloat("Sword Damage");
        //set number of health restores
        player.numberOfHealthRestores = PlayerPrefs.GetInt("Number of Health Restores");
        //set time travel player damage
        timeTravelV2.timeTravelPlayerDamage = PlayerPrefs.GetFloat("Time Travel Player Damage");
        // set time travel enemy damage
        timeTravelV2.timeTravelEnemyDamage = PlayerPrefs.GetFloat("Time Travel Enemy Damage");
        levelThreeComplete = false;
    }
    
    private void Update()
    {
        // check if all keys have been found and if so load the next scene
        if (keyCount == 4)
        {
            Debug.Log("Level Three Complete");
            SceneManager.LoadScene(4);
        }
    }
    
    // method for incrementing the key count
    public void KeyFound()
    {
        Debug.Log("Key Found");
        keyCount++;
        Debug.Log("Key Count: " + keyCount);
    }
}
