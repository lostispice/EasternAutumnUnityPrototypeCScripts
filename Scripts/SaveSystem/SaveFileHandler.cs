using System.IO;
using UnityEngine;

/// <summary>
/// This script is used to handle saving player profile data to the local disk.
/// It is also used when loading player profile data from the local disk, if a JSON save file exists in the save directory location.
/// Future versions could support a multiple-slot save system under player-specific save names.
/// An instance of this script is automatically created by SaveManager.cs when the ProfileSelect screen is first loaded.
/// </summary>
public class SaveFileHandler
{
    /// <summary>
    /// These variables determine the path and location of the save directory used for storing save data.
    /// </summary>
    private string dataDirPath = "";
    private string dataFileName = "";

    /// <summary>
    /// Constructor, called by SaveManager.cs.
    /// </summary>
    /// <param name="dataDirPath"></param>
    /// <param name="dataFileName"></param>
    public SaveFileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    /// <summary>
    /// Loads profile data from the local disk.
    /// Future versions could implement code that support a multiple-slot save system with specific save names.
    /// </summary>
    public PlayerProfile Load()
    {
        dataFileName = "save.json"; // Generic save name for this version
                
        string fullPath = Path.Combine(dataDirPath, dataFileName); // Uses Path.Combine for cross-OS compatibility
        PlayerProfile loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<PlayerProfile>(dataToLoad);
            }
            catch
            {
                Debug.LogError("File could not be loaded");
            }
        }
        return loadedData;
    }

    /// <summary>
    /// Saves profile data to the local disk.
    /// Future versions could implement code that support a multiple-slot save system with specific save names.
    /// </summary>
    /// <param name="player"></param>
    public void Save(PlayerProfile player)
    {
        dataFileName = "save.json"; // Generic save name for this version
                
        string fullPath = Path.Combine(dataDirPath, dataFileName); // Uses Path.Combine for cross-OS compatibility
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            string dataToStore = JsonUtility.ToJson(player, true); // serialise object to Json file
                        
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) // write serialised data to file
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch
        {
            Debug.LogError("File could not be saved to:" + fullPath + "\n");
        }
    }
}
