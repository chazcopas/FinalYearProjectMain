using System;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    // bool to track if the info menu is open
    private bool infoMenuOpen;
    public PauseMenu pauseMenu;
    public GameObject infoMenuPanel;
    
    private void Awake()
    {
        // set the info menu to be closed when scene is loaded
        infoMenuPanel.SetActive(false);
        infoMenuOpen = false;
    }

    private void Update()
    {
        // close info menu if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && infoMenuOpen)
        {
            Debug.Log("Info Menu Closed");
            infoMenuPanel.SetActive(false);
            infoMenuOpen = false;
            pauseMenu.ReturnToPauseMenu();
        }
    }
    
    // methods for opening and closing the info menu
    public void OpenInfo()
    {
        infoMenuOpen = true;
        infoMenuPanel.SetActive(true);
    }
    
    public void CloseInfo()
    {
        infoMenuOpen = false;
        infoMenuPanel.SetActive(false);
        pauseMenu.ReturnToPauseMenu();
    }
}
