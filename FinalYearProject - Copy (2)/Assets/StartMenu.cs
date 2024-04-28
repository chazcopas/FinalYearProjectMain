using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    private GameObject startMenu;
    private GameObject optionsMenu;
    // bool to track if the options menu is open
    private bool optionsOpen;
    
    private void Start()
    {
        // set the start menu to be open when scene is loaded
        Debug.Log("Start Menu Loaded");
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
        optionsOpen = false;
    }
    
    public void StartGame()
    {
        Debug.Log("Game Started");
        // load the first level
        SceneManager.LoadScene(2);
    }
    
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
    // methods for opening and closing the options menu
    public void OpenOptions()
    {
        Debug.Log("Options Opened");
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
        optionsOpen = true;
    }
    
    public void CloseOptions()
    {
        Debug.Log("Options Closed");
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
        optionsOpen = false;
    }
}
