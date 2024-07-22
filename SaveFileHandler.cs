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

    public PlayerProfile Load()
    {
        // TODO - This file name should be dynamic for different characters/profiles. Presently this uses a premade file
        dataFileName = "Arno.json";

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

    public void Save(PlayerProfile player)
    {
        // Sets save file name based on player character name
        dataFileName = player.playerName + ".json";
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
}
