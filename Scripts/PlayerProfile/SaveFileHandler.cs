using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFileHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public SaveFileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    // Loads a profile using a player name + json (e.g. name.json)
    public PlayerProfile Load()
    {
        // Current version uses a fixed save file name. Future versions could support a multiple save-slot system
        dataFileName = "save.json";

        // Uses Path.Combine for cross-OS compatibility
        string fullPath = Path.Combine(dataDirPath, dataFileName);
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

    /*
     * // Loads a profile using a player name + json (e.g. name.json) - Multiple profile version
    public PlayerProfile Load(string profileID)
    {        
        dataFileName = "save" + profileID + ".json"; // e.g. save0.json will correspond to profile ID 0

        // Uses Path.Combine for cross-OS compatibility
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
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
    }*/

    // Saves profile data
    public void Save(PlayerProfile player)
    {
        dataFileName = "save.json";

        // Uses Path.Combine for cross-OS compatibility
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialise object to Json file
            string dataToStore = JsonUtility.ToJson(player, true);

            // write serialised data to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
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

    /*
     * // Saves profile data - Multiple profile version
    public void Save(PlayerProfile player, string profileID)
    {
        // Sets save file name based on profileID
        dataFileName = "save" + profileID + ".json"; // e.g. save0.json will correspond to profile ID 0;

        // Uses Path.Combine for cross-OS compatibility
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialise object to Json file
            string dataToStore = JsonUtility.ToJson(player, true);

            // write serialised data to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
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
    }*/

    /*
     * // Used by the profile selection screen to list all saved profiles
    public Dictionary<string, PlayerProfile> LoadAllProfiles()
    {
        Dictionary<string, PlayerProfile> profiles = new Dictionary<string, PlayerProfile>();

        // Loops over all directories in the save folder path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;

            // TODO - insert defensive programming that checks for folders that exist in the directory that do not contain valid save/JSON files

            // Loads a profile and adds it ot the profiles dictionary
            PlayerProfile player = Load(profileID);
            if (profileID != null)
            {
                profiles.Add(profileID, player);
            }
            else
            {
                Debug.LogError("Something went wrong when attempting to load profile ID " + profileID);
            }

        }

        return profiles;
    }*/
}
