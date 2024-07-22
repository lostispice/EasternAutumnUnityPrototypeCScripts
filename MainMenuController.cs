using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Current Active player
    PlayerProfile player;

    // Placeholder value for playerPrefs
    [SerializeField] string playerName;
    // Placeholder values, used to test award system.
    // TODO - Create dictionary that contains award data based on difficulty. public SerializableDictionary<key, bool> achievements;
    [SerializeField] bool target;
    [SerializeField] bool award;

    // Start is called before the first frame update
    void Start()
    {
        // Simulates loading a profile/character named "Arno" from the Profile page, see SaveManager.cs. Currently uses premade save file
        SaveManager.instance.LoadProfile();
        player = SaveManager.instance.player;
        playerName = player.playerName;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Uses PlayerPrefs, consider reworking this with new save system
    public void StartGameplaySession(int difficulty)
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameplaySession");
    }

}
