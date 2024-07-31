using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    // public string profileID; TODO - implement save slots
    public string playerName;
    public SerializableDictionary<string, bool> awards;
    public SerializableDictionary<int, bool> extraLives;

    // Constructor
    public PlayerProfile()
    {
        this.playerName = "Arno"; // Default player name
        this.awards = new SerializableDictionary<string, bool> // Uses SerializableDictionary.cs. String uses a simple code = Difficulty Number (0 - 3) + T (Target) or A (Award).
        {
            { "0T", false },
            { "0A", false },
            { "1T", false },
            { "1A", false },
            { "2T", false },
            { "2A", false },
            { "3T", false },
            { "3A", false }
        };
        this.extraLives = new SerializableDictionary<int, bool> // Uses SerializableDictionary.cs. Int refers to difficulty level this extra life was earned.
        {
            { 0, false },
            { 1, false },
            { 2, false },
            { 3, false }
        };
    }

    /*
     * Delete all unused scripts when submitting code for marking
     * 
     * // Constructor that requires nameInput string. TODO - Intended to be used with saveslot system
    public PlayerProfile(string nameInput)
    {
        this.playerName = nameInput;        
        this.difficulty = 0; // Default difficulty (Easy)
        this.awards = new SerializableDictionary<string, bool>
        {
            { "0T", false },
            { "0A", false },
            { "1T", false },
            { "1A", false },
            { "2T", false },
            { "2A", false },
            { "3T", false },
            { "3A", false }
        };
        this.extraLives = new SerializableDictionary<int, bool> // Uses SerializableDictionary.cs. int refers to difficulty level this extra life was earned 
        {
            { 0, false },
            { 1, false },
            { 2, false },
            { 3, false }
        };
    }*/

    /*
     * // ProfileID getter - TODO: Save Slots 
    public string GetProfileID()
    {
        return this.profileID;
    }*/
}
