using UnityEngine;

public class KeybindsMenu : MonoBehaviour
{
    public SettingsMenu settingsMenu;
    public GameObject keybindsMenuPanel;
    // bool to check if the keybinds menu is open
    private bool keybindsMenuOpen;
    
    private void Awake()
    {
        // set the keybinds menu to be closed when scene is loaded
        keybindsMenuPanel.SetActive(false);
        keybindsMenuOpen = false; 
    }
    
    private void Update()
    {
        // close keybinds menu if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && keybindsMenuOpen)
        {
            Debug.Log("Keybinds Menu Closed");
            keybindsMenuPanel.SetActive(false);
            keybindsMenuOpen = false;
            settingsMenu.ReturnToSettingsMenu();
        }
    }
    
    // methods for opening and closing the keybinds menu
    public void OpenKeybinds()
    {
        keybindsMenuOpen = true;
        keybindsMenuPanel.SetActive(true);
    }
    
    public void CloseKeybinds()
    {
        keybindsMenuOpen = false;
        keybindsMenuPanel.SetActive(false);
        settingsMenu.ReturnToSettingsMenu();
    }
}
