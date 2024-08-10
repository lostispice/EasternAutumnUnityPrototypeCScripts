using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages functionality within the ProfileSelect screen.
/// It is loaded alongside SaveManager.cs and interacts with it.
/// </summary>
public class ProfileSelectController : MonoBehaviour, ISave
{
    /// <summary>
    /// This variable handles the Player Name textbox displayed to the player.
    /// It is used to display the current player name retrieved from the save file (if any) and allows the user to key-in a new name.
    /// </summary>
    [SerializeField] TMP_InputField playerName;

    /// <summary>
    /// Unity calls this method automatically when the ProfileSelect screen is first loaded.
    /// </summary>
    private void Start()
    {
        CurrentProfile();
    }

    /// <summary>
    /// Loads the game's save.json file (or creates one if one doesn't exist)
    /// </summary>
    public void CurrentProfile()
    {
        try
        {
            SaveManager.instance.LoadProfile();
        }
        catch
        {
            SaveManager.instance.NewProfile();
            ResetOptions();
        }
        LoadProfile(SaveManager.instance.player);
    }

    /// <summary>
    /// Saves the new player name.
    /// Uses the ISave interface.
    /// </summary>
    /// <param name="player"></param>
    public void SaveProfile(PlayerProfile player)
    {
        player.playerName = playerName.text;
    }

    /// <summary>
    /// Retreives the saved player name and displays it on screen.
    /// Uses the ISave interface.
    /// </summary>
    /// <param name="player"></param>
    public void LoadProfile(PlayerProfile player) 
    {
        playerName.text = player.playerName;
    }

    /// <summary>
    /// Reverts the playerName input box to the current saved name.
    /// </summary>
    public void UndoName()
    {
        LoadProfile(SaveManager.instance.player);
    }
        
    /// <summary>
    /// Creates a blank "default" profile and overwrites the old one.
    /// </summary>
    public void ResetProfile()
    {
        SaveManager.instance.NewProfile();
        SaveManager.instance.SaveProfile();
        ResetOptions();
    }

    /// <summary>
    /// Resets gameplay options to their default values. Future versions could reference an external OptionsDefault config file.
    /// </summary>
    public void ResetOptions()
    {
        AudioListener.volume = 1.0f;
        PlayerPrefs.SetInt("timerModifier", 60);
        PlayerPrefs.SetInt("targetModifier", 5);
        PlayerPrefs.SetInt("livesModifier", 3);
    }

    /// <summary>
    /// Saves any changes to the player's name and loads the main menu screen.
    /// </summary>
    public void PlayGame()
    {
        SaveProfile(SaveManager.instance.player);
        SaveManager.instance.SaveProfile();
        SceneManager.LoadScene("MainMenu");
    }
}
