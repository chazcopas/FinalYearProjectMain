using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Ui for the level start screen   
    public GameObject LevelStartUI;
    
    // Start is called before the first frame update
    void Start()
    {
        // pause the game and show the level start screen
        Debug.Log("Level Start Loaded");
        Time.timeScale = 0;
        LevelStartUI.SetActive(true);   
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    // method for starting the level and resuming the game
    public void StartLevel()
    {
        Debug.Log("Level Started");
        Time.timeScale = 1;
        LevelStartUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
