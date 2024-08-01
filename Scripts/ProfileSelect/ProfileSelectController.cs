using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ProfileSelectController : MonoBehaviour, ISave
{
    [SerializeField] TMP_InputField playerName;

    private void Start()
    {
        CurrentProfile();
    }

    private void Update()
    {
    }

    // Loads the game's save.json file (or creates one if one doesn't exist)
    public void CurrentProfile()
    {
        try
        {
            SaveManager.instance.LoadProfile();
        }
        catch
        {
            SaveManager.instance.NewProfile();
        }
        LoadProfile(SaveManager.instance.player);
    }

    // Saves new player name (ISave)
    public void SaveProfile(PlayerProfile player)
    {
        player.playerName = playerName.text;
    }

    // Retreives the saved player name and displays it on screen (ISave)
    public void LoadProfile(PlayerProfile player) 
    {
        playerName.text = player.playerName;
    }

    // Returns the playerName input box to the currently saved name
    public void UndoName()
    {
        LoadProfile(SaveManager.instance.player);
    }
        
    // Creates a blank "default" profile and overwrites the old one.
    public void ResetProfile()
    {
        SaveManager.instance.NewProfile();
        SaveManager.instance.SaveProfile();
        ResetOptions();
    }

    // Resets gameplay options to default
    public void ResetOptions()
    {
        AudioListener.volume = 1.0f;
        PlayerPrefs.SetInt("timerModifier", 60);
        PlayerPrefs.SetInt("targetModifier", 5);
        PlayerPrefs.SetInt("livesModifier", 3);
    }

    // Saves any changes to the player's name and loads the main menu
    public void PlayGame()
    {
        SaveProfile(SaveManager.instance.player);
        SaveManager.instance.SaveProfile();
        SceneManager.LoadScene("MainMenu");
    }

}

/*
 * // TODO - Implement multiple save profiles system
public class ProfileSelectController : MonoBehaviour
{
    [SerializeField] SaveSlot[] saveSlots;
    [SerializeField] TextMeshProUGUI activePlayerName;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        LoadProfiles();
    }

    public void LoadProfiles()
    {
        // Load all saved profiles
        Dictionary<string, PlayerProfile> savedPlayers = SaveManager.instance.RetrievePlayerProfiles();

        // Loop through all four slots and update their values
        foreach (SaveSlot saveSlot in saveSlots)
        {
            PlayerProfile player = null;
            savedPlayers.TryGetValue(saveSlot.profileID, out player);
            saveSlot.ProfileChecker(player);
        }
    }

    public void UpdateActiveName()
    {
        this.activePlayerName.text = SaveManager.instance.player.playerName;
    }

}*/