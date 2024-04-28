using UnityEngine;

public class KeybindsStart : MonoBehaviour
{
    public GameObject startPage;
    public GameObject KeyBindsPage;
    // bool to check if the keybinds menu is open
    public bool open;
    
    void Start()
    {
        KeyBindsPage.SetActive(false);
        open = false;
    }
    
    void Update()
    {
        // close keybinds menu if escape is pressed
        if (open && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToStart();
        }
    }
    // method for returning to the start page
    public void ReturnToStart()
    {
        open = false;
        startPage.GetComponent<StartPage>().Return();
    }
}
