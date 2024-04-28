using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    public GameObject startPage;

    public GameObject KeyBindsPage;

    // Start is called before the first frame update
    void Start()
    {
        // set the start menu to be open when scene is loaded
        startPage.SetActive(true);
    }

    // Update is called once per frame
    public void StartGame()
    {
        // load the first level
        startPage.SetActive(false);
        KeyBindsPage.SetActive(false);
        SceneManager.LoadScene(1);
    }
    
    public void KeyBinds()
    {
        startPage.SetActive(false);
        KeyBindsPage.SetActive(true);
        KeyBindsPage.GetComponent<KeybindsStart>().open = true;
    }
    
    public void Return()
    {
        startPage.SetActive(true);
        KeyBindsPage.SetActive(false);
    }
}
