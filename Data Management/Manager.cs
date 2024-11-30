using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// attach to datamanager empty object in main menu
public class Manager : MonoBehaviour
{
    // save location, persistent data path allows for it to work on any device
    private static string savePath;

    private static Manager instance;

    // make manager singleton
    public static Manager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Manager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  
        }
        else
        {
            DontDestroyOnLoad(gameObject);  
            instance = this;
        }

        // have to initialize save path on awake so datamanager can persist among all scenes
        savePath = Application.persistentDataPath + "/saveData.json";
    }

    // save data
    public static void saveData(List<Data> data)
    {
        string json = JsonUtility.ToJson(new SaveDataWrapper
        {
            objects = data,
        }
        );
        Debug.Log("Data Saved to Json");
        File.WriteAllText(savePath, json );
    }

    // load data
    public static List<Data> LoadData()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No Save File Found");
            return new List<Data>();
        }

        Debug.Log("Accessing Json");
        string json = File.ReadAllText(savePath);
        SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(json);
        return wrapper.objects;
    }
}

[Serializable]
public class SaveDataWrapper
{
    public List<Data> objects;
}
