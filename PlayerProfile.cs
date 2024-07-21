using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Requires rework. Main Menu may need its own MainMenuController instead of interfacing directly wtih PlayerProfile?
[System.Serializable]
public class PlayerProfile : MonoBehaviour
{    
    public string playerName;
    [SerializeField] int difficulty;
    // Placeholder values, used to test SaveManger.cs. TODO - Create dictionary that contains award data based on difficulty
    // public SerializableDictionary<key, bool> achievements;
    public bool target;
    public bool award;

    // Empty constructor
    public PlayerProfile()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameplaySession(int difficulty)
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameplaySession");
    }
}
