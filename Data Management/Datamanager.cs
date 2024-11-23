using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// attach to datamanager empty object in main menu
public class Datamanager : MonoBehaviour
{
    // save location, persistent data path allows for it to work on any device
    private static string savePath;

    private void Awake()
    {
        if (FindObjectsOfType<Datamanager>().Length > 1)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        // have to initialize save path on awake so datamanager can persist among all scenes
        savePath = Application.persistentDataPath + "/saveData.json";
    }

    // save data
    public static void saveData(List<Data> data)
    {
        string json = JsonUtility.ToJson(new SaveDataWrapper
        {
            imageTargets = data,
        }
        );
        Debug.Log("Data Saved to Json");
        File.WriteAllText(savePath, json );
    }

    // load data
    public static List<Data> LoadData()
    {
        if(!File.Exists(savePath))
            return new List<Data>();

        Debug.Log("Accessing Json");
        string json = File.ReadAllText(savePath);
        SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(json);
        return wrapper.imageTargets;
    }
}

[Serializable]
public class SaveDataWrapper
{
    public List<Data> imageTargets;
}
