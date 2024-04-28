using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour  
{
    public PauseMenu pauseMenu;
    public GameObject settingsMenuPanel;
    // bool to track if the settings menu is open
    private bool settingsMenuOpen;
    public KeybindsMenu keybindsMenu;
    public Slider volumeSlider;
    
    private float volume;
    
    private void Awake()
    {
        // set the settings menu to be closed when scene is loaded
        settingsMenuPanel.SetActive(false);
        settingsMenuOpen = false; 
    }
    
    private void Update()
    {
        // close settings menu if escape is pressed and the settings menu is open
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenuOpen)
        {
            Debug.Log("Settings Menu Closed");
            settingsMenuPanel.SetActive(false);
            settingsMenuOpen = false;
            pauseMenu.ReturnToPauseMenu();
        }
        // change volume depending on slider value
        AudioListener.volume = volumeSlider.value;
    }
    
    // sets volume and is called when the slider is moved
    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
    
    // methods for moving between menus
    public void OpenSettings()
    {
        settingsMenuOpen = true;
        settingsMenuPanel.SetActive(true);
    }
    
    public void OpenKeybinds()
    {
        settingsMenuOpen = false;
        settingsMenuPanel.SetActive(false);
        keybindsMenu.OpenKeybinds();
    }
    
    public void CloseSettings()
    {
        settingsMenuOpen = false;
        settingsMenuPanel.SetActive(false);
        pauseMenu.ReturnToPauseMenu();
    }
    
    public void ReturnToSettingsMenu()
    {
        settingsMenuOpen = true;
        settingsMenuPanel.SetActive(true);
    }
}
