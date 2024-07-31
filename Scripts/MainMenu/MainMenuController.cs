using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Starts a gameplay session using a selected difficulty level
    public void StartGameplaySession(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameplaySession");
    }

    // Return to profile screen
    public void ReturnToProfile()
    {
        SceneManager.LoadScene("ProfileSelect");
    }

    // Exits game
    public void ExitGame()
    {
        Application.Quit();
    }
}
