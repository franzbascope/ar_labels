using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vuforia;

// attach to save manager empty object in main menu
public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;

    // make saveManager a singleton
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // correctly saving, but is adding (Clone) to the end of object names causing issues with prefabs
    public void Save(GameObject imageTarget)
    {

        if (imageTarget == null)
        {
            Debug.Log("No Image Targets in Scene");
            return;
        }

        List<Data> saveData = new List<Data>();

        // save data for each image target
        foreach (Transform child in imageTarget.transform)
        {
            // find prefab name, remove "(Clone)" so it can load the prefab later
            string prefabName = child.name.Replace("(Clone)", "").Trim();

            saveData.Add(new Data(imageTarget.name, prefabName, child));
        }

        Manager.saveData(saveData);
    }
}