using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///  This script manages non-awards functionality within the Main Menu screen.
///  It is loaded alongside AwardsController.cs
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// These variables are used by the Options window.
    /// </summary>
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI volumeNum;
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI timerNum;
    [SerializeField] Slider targetSlider;
    [SerializeField] TextMeshProUGUI targetNum;
    [SerializeField] Slider livesSlider;
    [SerializeField] TextMeshProUGUI livesNum;

    /// <summary>
    /// Unity calls this method automatically when the MainMenu screen is first loaded.
    /// </summary>
    private void Start()
    {
        GetOptions();
        OptionsListener();
    }

    /// <summary>
    /// Starts a gameplay session using a selected difficulty level.
    /// </summary>
    /// <param name="difficulty"></param>
    public void StartGameplaySession(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameplaySession");
    }

    /// <summary>
    /// Returns to profile screen.
    /// </summary>
    public void ReturnToProfile()
    {
        SceneManager.LoadScene("ProfileSelect");
    }

    /// <summary>
    /// Exits the game. SaveManager.cs will also save the player's data.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Applies AddListener to all Options sliders and updates the values in their associated textboxes.
    /// AddListener allows Unity to process input from settings sliders.
    /// </summary>
    public void OptionsListener()
    {
        volumeSlider.onValueChanged.AddListener((v) => {
            volumeNum.text = v.ToString("0.00");
        });
        timerSlider.onValueChanged.AddListener((v) => {
            timerNum.text = v.ToString("0");
        });
        targetSlider.onValueChanged.AddListener((v) => {
            targetNum.text = v.ToString("0");
        });
        livesSlider.onValueChanged.AddListener((v) => {
            livesNum.text = v.ToString("0");
        });
    }

    /// <summary>
    /// Fetches any saved options values and updates sliders & text accordingly.
    /// Future versions could retrieve Options data directly from a player-specific save file.
    /// </summary>
    public void GetOptions()
    {
        volumeSlider.value = AudioListener.volume;
        volumeNum.text = AudioListener.volume.ToString("F2");
        try
        {
            timerNum.text = PlayerPrefs.GetInt("timerModifier").ToString();
            timerSlider.value = PlayerPrefs.GetInt("timerModifier");
            targetNum.text = PlayerPrefs.GetInt("targetModifier").ToString();
            targetSlider.value = PlayerPrefs.GetInt("targetModifier");
            livesNum.text = PlayerPrefs.GetInt("livesModifier").ToString();
            livesSlider.value = PlayerPrefs.GetInt("livesModifier");
        }
        catch
        {
            DefaultOptions(); // Failsafe: if no saved options data exists, the game will load default values. Beware of infinite loops.
        }
    }

    /// <summary>
    /// Saves the current slider values in the Options window.
    /// Future versions could save this directly to a player-specific save file.
    /// </summary>
    public void SaveOptions()
    {        
        AudioListener.volume = volumeSlider.value; // AudioListener is global (persistent across screens)
        PlayerPrefs.SetInt("timerModifier", Convert.ToInt32(timerSlider.value));
        PlayerPrefs.SetInt("targetModifier", Convert.ToInt32(targetSlider.value));
        PlayerPrefs.SetInt("livesModifier", Convert.ToInt32(livesSlider.value));
    }

    /// <summary>
    /// Returns all Options values to default values.
    /// Future versions could reference an OptionsDefault config file.
    /// </summary>
    public void DefaultOptions()
    {
        AudioListener.volume = 1.0f;
        PlayerPrefs.SetInt("timerModifier", 60);
        PlayerPrefs.SetInt("targetModifier", 5);
        PlayerPrefs.SetInt("livesModifier", 3);
        GetOptions(); // Returns all sliders/textboxes to default valuse. Beware of infinite loops.
    }
}
