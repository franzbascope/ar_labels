using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// https://www.youtube.com/watch?v=aUi9aijvpgs
public class FileDataHandler
{
    private string directoryPath = "";
    private string fileName = "";

    public FileDataHandler(string directoryPath, string fileName)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
    }

    public Data Load()
    {
        string path = Path.Combine(directoryPath, fileName);
        Data loadedData = null;

        if (File.Exists(path)) 
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<Data>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("failed to laod data from file" + e);
            }
        }

        return loadedData;
    }

    public void Save(Data data)
    {
        string path = Path.Combine(directoryPath, fileName);
        try
        {
            // create directory
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            string dataToStore = JsonUtility.ToJson(data, true);

            // write to system
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("error when trying to save" + e);
        }
    }
}
