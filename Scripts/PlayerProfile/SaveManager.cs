using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public PlayerProfile player;

    // public string activeProfileID; TODO - Implement multiple save slots

    // Save file name, should correspond with "player character name + .json"
    [SerializeField] string fileName;

    // Used to list all scenes that use ISave behaviour.
    [SerializeField] List<ISave> saveObjects;

    // Used to write save data to JSON file
    [SerializeField] SaveFileHandler saveFileHandler;    

    // Singleton
    public static SaveManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {        
        this.saveFileHandler = new SaveFileHandler(Application.persistentDataPath, fileName);
        this.saveObjects = FindAllSaveObjects();
    }

    // Creates list of all scenes/scripts that use ISave interface 
    private List<ISave> FindAllSaveObjects()
    {
        IEnumerable<ISave> saveObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISave>();

        return new List<ISave>(saveObjects);
    }

    // OnSceneLoaded (Scene scene) <- Consider: Saving profile data every time a scene is loaded

    // Changes the currently selected profile
    /*public void ChangeActiveProfile(string profileID)
    {
        this.activeProfileID = profileID;
        LoadProfile();
    }*/
    
    // Creates a new player profile
    public void NewProfile()
    {
        this.player = new PlayerProfile();
    }

    // Loads a profile using a playerID, across all scenes that use the ISave interface
    public void LoadProfile()
    {
        this.player = saveFileHandler.Load();

        foreach (ISave saveObj in saveObjects)
        {
            saveObj.LoadProfile(player);
        }
    }

    /*
     * // Multiple profile version
    public void LoadProfile()
    {
        this.player = saveFileHandler.Load(activeProfileID);

        foreach (ISave saveObj in saveObjects)
        {
            saveObj.LoadProfile(player);
        }
    }*/

    // Saves a profile using a playerID, across all scenes that use the ISave interface
    public void SaveProfile()
    {
        foreach (ISave saveObj in saveObjects)
        {
            saveObj.SaveProfile(player);
        }

        saveFileHandler.Save(player);
    }

    /*//  Multiple profile version
    public void SaveProfile()
    {
        foreach (ISave saveObj in saveObjects)
        {
            saveObj.SaveProfile(player);
        }

        saveFileHandler.Save(player, activeProfileID);
    }*/

    // Saves player profile on application exit
    private void OnApplicationQuit()
    {
        SaveProfile();
    }

    /*
     * // Multiple profile version
    public Dictionary<string, PlayerProfile> RetrievePlayerProfiles()
    {
        return saveFileHandler.LoadAllProfiles();
    }*/
}
