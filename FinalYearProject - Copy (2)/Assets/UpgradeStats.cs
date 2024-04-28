using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{
    public GameObject playerTimeTravel;
    public GameObject player;
    public GameObject gunObject;
    public GameObject swordObject;
    
    // ui elements
    public GameObject upgradePanel;
    
    private bool panelOpen;

    private void Awake()
    {
        upgradePanel.SetActive(false);
        panelOpen = false;
        Debug.Log("Upgrade Panel is awake");
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.U) && !panelOpen)
        // {
        //     panelOpen = true;
        //     Debug.Log("Upgrade Panel");
        //     Time.timeScale = 0;
        //     upgradePanel.SetActive(true);
        //     Cursor.visible = true;
        //     Cursor.lockState = CursorLockMode.None;
        // }
    }
    
    public void OpenPanel()
    {
        panelOpen = true;
        Time.timeScale = 0;
        upgradePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void ClosePanel()
    {
        panelOpen = false;
        Time.timeScale = 1;
        upgradePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
    // gun upgrades
    public void UpgradeGunDamage(int amount)
    {
        gunObject.GetComponent<GunV2>().IncreaseDamage(amount); 
        ClosePanel();
    }
    
    public void ReduceGunSpread(float amount)
    {
        gunObject.GetComponent<GunV2>().ReduceSpread(amount);
        ClosePanel();
    }
    
    public void ReduceGunReloadTime(float amount)
    {
        gunObject.GetComponent<GunV2>().ReduceReloadTime(amount);
        ClosePanel();
    }
    
    
    //sword upgrades
    public void UpgradeSwordDamage(int amount)
    {
        swordObject.GetComponent<SwordsV2>().IncreaseDamage(amount);
        ClosePanel();
    }
    
    public void ReduceStaminaCost(float amount)
    {
        swordObject.GetComponent<SwordsV2>().ReduceStaminaCost(amount);
        ClosePanel();
    }
    
    public void ReduceTimeBetweenAttacks(float amount)
    {
        swordObject.GetComponent<SwordsV2>().ReduceTimeBetweenAttacks(amount);
        ClosePanel();
    }
    
    
    // player upgrades
    public void IncreaseMaxHealth(float amount)
    {
        player.GetComponent<Player>().IncreaseMaxHealth(amount);
        ClosePanel();
    }
    
    public void IncreaseMaxStamina(float amount)
    {
        player.GetComponent<Player>().increaseMaxStamina(amount);
        ClosePanel();
    }
    
    public void IncreaseHealthRestoreBy(float amount)
    {
        player.GetComponent<Player>().increaseRestoreBy(amount);
        ClosePanel();
    }
    
    // time travel upgrades
    public void ReduceTimeTravelCooldown(float amount)
    {
        playerTimeTravel.GetComponent<TimeTravelV2>().ReduceTimeTravelCooldownPeriod(amount);
        ClosePanel();
    }
    
    public void ReducePlayerTimeTravelDamage(float amount)
    {
        playerTimeTravel.GetComponent<TimeTravelV2>().ReducePlayerTimeTravelDamage(amount);
        ClosePanel();
    }
    
}
