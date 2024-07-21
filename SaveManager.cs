using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO - Not working properly, requires rework
public class SaveManager : MonoBehaviour
{
    // Save file name, should correspond with "player character name + .json"
    [SerializeField] string playerName;

    private PlayerProfile playerProfile;
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
        this.saveFileHandler = new SaveFileHandler(Application.persistentDataPath, playerName);
        this.saveObjects = FindAllSaveObjects();
    }

    public void NewProfile()
    {
        this.playerProfile = new PlayerProfile();
    }

    public void LoadProfile()
    {
        this.playerProfile = saveFileHandler.Load();

        foreach (ISave saveObj in saveObjects)
        {
            saveObj.LoadProfile(playerProfile);
        }
    }

    public void SaveProfile()
    {
        playerName = PlayerPrefs.GetString("playerName") + ".json";
        // Causes null error
        foreach (ISave saveObj in saveObjects)
        {
            saveObj.SaveProfile(ref playerProfile);
        }

        saveFileHandler.Save(playerProfile);
    }

    // Creates list of all scenes/scripts that use saving behaviour
    private List<ISave> FindAllSaveObjects()
    {
        IEnumerable<ISave> saveObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISave>();

        return new List<ISave>(saveObjects);
    }
}
