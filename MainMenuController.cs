using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Current Active player
    [SerializeField] PlayerProfile player;

    // TODO - Create dictionary that contains award data based on difficulty. public SerializableDictionary<key, bool> achievements;

    // Start is called before the first frame update
    void Start()
    {
        // SaveManager.instance.NewProfile("Arno"); // Used to create a save file for testing purposes        
        SaveManager.instance.LoadProfile("Arno"); // TODO - Implement "profile selection" screen to select a profile to load. Currently uses a premade save file "arno.json"
        player = SaveManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Starts a gameplay session using a selected difficulty level
    public void StartGameplaySession(int difficulty)
    {
        SaveManager.instance.player.difficulty = difficulty;
        SceneManager.LoadScene("GameplaySession");
    }

    // Exits game
    public void ExitGame()
    {
        Application.Quit();
    }
}
