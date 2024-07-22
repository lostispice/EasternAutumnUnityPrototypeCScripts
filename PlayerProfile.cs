using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
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
        this.playerName = "";
        // Default difficulty (Easy)
        this.difficulty = 0;
        this.target = false;
        this.award = false;
    }

    // Constructor that requires nameInput string. TODO - implement this with profile selection screen
    public PlayerProfile(string nameInput)
    {
        this.playerName = nameInput;
        // Default difficulty (Easy)
        this.difficulty = 0;
        this.target = false;
        this.award = false;
    }
}
