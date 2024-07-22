using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO - Not working properly, requires rework
public class SaveManager : MonoBehaviour
{
    public PlayerProfile player;

    // Save file name, should correspond with "player character name + .json"
    [SerializeField] string fileName;

    // Used to list all scenes that use ISave behaviour.
    private List<ISave> saveObjects;

    private SaveFileHandler saveFileHandler;

    // Singleton class
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

    public void NewProfile(string nameInput)
    {
        this.player = new PlayerProfile(nameInput);
    }

    public void LoadProfile()
    {
        this.player = saveFileHandler.Load();

        foreach (ISave saveObj in saveObjects)
        {
            saveObj.LoadProfile(player);
        }
    }

    public void SaveProfile()
    {
        foreach (ISave saveObj in saveObjects)
        {
            saveObj.SaveProfile(player);
        }

        saveFileHandler.Save(player);
    }

    // Saves player profile on application exit
    private void OnApplicationQuit()
    {
        SaveProfile();
    }

    // Creates list of all scenes/scripts that use saving behaviour
    private List<ISave> FindAllSaveObjects()
    {
        IEnumerable<ISave> saveObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISave>();

        return new List<ISave>(saveObjects);
    }
}
