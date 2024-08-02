using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This script handles the save system used throughout the game, including saving and retrieving data from the current active profile.
/// The Singleton pattern is used so that only a single instance exists and is accessed globally throughout the game.
/// It is loaded alongside ProfileSelectController.cs when the ProfileSelect screen is loaded.
/// </summary>
public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// This variable is the "current" active player profile in use.
    /// Future versions could support a multiple-save slot system that loads a specific player account.
    /// </summary>
    public PlayerProfile player;

    /// <summary>
    /// This variable determines the save file name, originally intended to support a multiple-save slot system (e.g. "player character name + .json")
    /// Currently, a generic "save.json" name is used instead. 
    /// </summary>
    [SerializeField] string fileName;

    /// <summary>
    /// This variable is used to list all scenes that use ISave behaviour.
    /// </summary>
    [SerializeField] List<ISave> saveObjects;

    /// <summary>
    /// This variable (script) is used to write save data to the local disk
    /// </summary>
    [SerializeField] SaveFileHandler saveFileHandler;    

    /// <summary>
    /// This variable is a singleton instance of the class.
    /// </summary>
    public static SaveManager instance { get; private set; }

    /// <summary>
    /// Unity calls this method before Start() to initialise the singleton instance before other scripts.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Unity calls this method automatically when the ProfileSelect screen is first loaded.
    /// </summary>
    void Start()
    {        
        this.saveFileHandler = new SaveFileHandler(Application.persistentDataPath, fileName); // Uses a Unity property to determine the directory for saving data
        this.saveObjects = FindAllSaveObjects();
    }

    /// <summary>
    /// Creates list of all scenes/scripts that use ISave interface 
    /// </summary>
    private List<ISave> FindAllSaveObjects()
    {
        IEnumerable<ISave> saveObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISave>();

        return new List<ISave>(saveObjects);
    }
    
    /// <summary>
    /// Creates a new player profile.
    /// Any previosly active profile will be disgarded and replaced with a blank profile.
    /// </summary>
    public void NewProfile()
    {
        this.player = new PlayerProfile();
    }

    /// <summary>
    /// Loads data and updates all scripts that use the ISave interface
    /// </summary>
    public void LoadProfile()
    {
        this.player = saveFileHandler.Load();

        foreach (ISave saveObj in saveObjects)
        {
            saveObj.LoadProfile(player);
        }
    }

    /// <summary>
    /// Saves data and updates all scripts that use the ISave interface
    /// </summary>
    public void SaveProfile()
    {
        foreach (ISave saveObj in saveObjects)
        {
            saveObj.SaveProfile(player);
        }

        saveFileHandler.Save(player);
    }

    /// <summary>
    /// Saves player profile when exiting the game.
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveProfile();
    }
}
