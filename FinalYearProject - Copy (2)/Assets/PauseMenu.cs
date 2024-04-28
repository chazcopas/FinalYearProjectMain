using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool pauseMenuOpen;
    public SettingsMenu settingsMenu;
    public InfoMenu infoMenu;
    
    public GameObject pauseMenuPanel;
    
    private void Awake()
    {
        // set the pause menu to be closed when scene is loaded
        pauseMenuPanel.SetActive(false);
        pauseMenuOpen = false;
        Debug.Log("Pause Menu Loaded");
    }
    
    private void Update()
    {
        // close pause menu if escape is pressed and the pause menu is open
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuOpen)
        {
            Debug.Log("Pause Menu Closed");
            Time.timeScale = 1;
            pauseMenuPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuOpen = false;
        }
        // open pause menu if escape is pressed and the pause menu is not open
        else if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuOpen)
        {
            Debug.Log("Pause Menu Opened");
            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseMenuOpen = true;
        }
    }
    // methods for opening and closing different menus
    public void ResumeGame()
    {
        pauseMenuOpen = false;
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void OpenSettings()
    {
        pauseMenuOpen = false;
        pauseMenuPanel.SetActive(false);
        settingsMenu.OpenSettings();
    }
    
    public void OpenInfoPage()
    {
        pauseMenuOpen = false;
        pauseMenuPanel.SetActive(false);
        infoMenu.OpenInfo();
    }
    
    public void ReturnToPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
        pauseMenuOpen = true;
    }
    
    // method for quitting the game
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
