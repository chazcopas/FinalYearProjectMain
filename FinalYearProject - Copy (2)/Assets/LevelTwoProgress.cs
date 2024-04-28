using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoProgress : MonoBehaviour
{
    private bool levelTwoComplete;
    public Player player;
    public TimeTravelV2 timeTravelV2;
    private void Awake()
    {
        levelTwoComplete = false;
       
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
    }
    
    // check if level two is complete
    private void Update()
    {
        if (levelTwoComplete)
        {
            Debug.Log("Level Two Complete");
            SceneManager.LoadScene(3);
        }
    }
    
    public void LevelTwoComplete()
    {
        // get variables and set values to playerprefs
        
        // get time travel cooldown
        PlayerPrefs.SetFloat("Time Travel Cooldown", timeTravelV2.timeTravelCooldown);
        // get player max health
        PlayerPrefs.SetFloat("Player Max Health", player.maxHealth);
        // get player max stamina
        PlayerPrefs.SetFloat("Player Max Stamina", player.maxStamina);
        // get player health restore by
        PlayerPrefs.SetFloat("Player Restore Health By", player.restoreHealthBy);
        // get player stamina restore by
        PlayerPrefs.SetFloat("Player Restore Stamina By", player.restoreStaminaBy);
        // get gun damage
        PlayerPrefs.SetFloat("Gun Damage", player.gun.damage);
        // get reload time
        PlayerPrefs.SetFloat("Gun Reload Time", player.gun.reloadTime);
        // get gun spread
        PlayerPrefs.SetFloat("Gun Spread", player.gun.spread);
        // get sword damage
        PlayerPrefs.SetFloat("Sword Damage", player.sword.damage);
        // get number of health restores
        PlayerPrefs.SetInt("Number of Health Restores", player.numberOfHealthRestores);
        // get time travel player damage
        PlayerPrefs.SetFloat("Time Travel Player Damage", timeTravelV2.timeTravelPlayerDamage);
        // get time travel enemy damage
        PlayerPrefs.SetFloat("Time Travel Enemy Damage", timeTravelV2.timeTravelEnemyDamage);
        levelTwoComplete = true;
    }
}
