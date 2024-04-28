using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneProgress : MonoBehaviour
{
    private bool levelOneComplete;
    public Player player;
    public TimeTravelV2 timeTravelV2;
    
    private void Awake()
    {
        levelOneComplete = false;
        PlayerPrefs.SetInt("Level One Complete", 0);
        PlayerPrefs.SetInt("Level Two Complete", 0);
        PlayerPrefs.SetInt("Level Three Complete", 0);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemiesPast = GameObject.FindGameObjectsWithTag("EnemyPast");
        Debug.Log("Enemies: " + enemies.Length + " Enemies Past: " + enemiesPast.Length);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Level One Finishing");
            LevelOneComplete();
        }
        if (levelOneComplete)
        {
            Debug.Log("Level One Complete");
            SceneManager.LoadScene(2);
        }
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemiesPast = GameObject.FindGameObjectsWithTag("EnemyPast");
        if ((enemies.Length == 0) && (enemiesPast.Length == 0))
        {
            LevelOneComplete();
        }
    }

    public void LevelOneComplete()
    {
        //get player object
        // GameObject player = GameObject.FindWithTag("Player");
        //get player script
        //get player time travel script
        
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
        
        PlayerPrefs.SetInt("Level One Complete", 1);
        Debug.Log("Level One Complete");
    }
}
