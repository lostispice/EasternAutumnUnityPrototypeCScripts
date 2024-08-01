using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI volumeNum;
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI timerNum;
    [SerializeField] Slider targetSlider;
    [SerializeField] TextMeshProUGUI targetNum;
    [SerializeField] Slider livesSlider;
    [SerializeField] TextMeshProUGUI livesNum;

    private void Start()
    {
        GetOptions();
        OptionsListener();
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

    // Applies AddListener to all Options objects
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

    // Fetches options values and updates sliders accordingly.
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
            DefaultOptions();
        }
    }

    public void SaveOptions()
    {
        // AudioListener is a static class and persistent across screens
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetInt("timerModifier", Convert.ToInt32(timerSlider.value));
        PlayerPrefs.SetInt("targetModifier", Convert.ToInt32(targetSlider.value));
        PlayerPrefs.SetInt("livesModifier", Convert.ToInt32(livesSlider.value));
    }

    // Returns all Options values to default, re-displays the values
    public void DefaultOptions()
    {
        AudioListener.volume = 1.0f;
        PlayerPrefs.SetInt("timerModifier", 60);
        PlayerPrefs.SetInt("targetModifier", 5);
        PlayerPrefs.SetInt("livesModifier", 3);
        GetOptions();
    }
}
