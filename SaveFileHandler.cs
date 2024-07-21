using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// TODO - Prerequisite scripts not working properly. Not tested.
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
                Debug.LogError("File could not be saved to:" + fullPath + "\n");
            }
        }
        return loadedData;
    }

    public void Save(PlayerProfile player)
    {
        // Uses Path.Combine for cross-OS compatibility
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialise object to Json file
            string dataToStore = JsonUtility.ToJson(player, true);
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
