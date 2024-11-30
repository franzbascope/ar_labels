using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataManager
{
    public List<ObjectData> objects;

    // implement singleton for objects
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }
    }
    // generate a private constructor and initialize objects
    private DataManager()
    {
        objects = new List<ObjectData>();
    }

    public void AddObject(GameObject gameObject)
    {
        ObjectData objectData = new ObjectData();
        objectData.name = gameObject.name;
        objectData.position = gameObject.transform.position;
        objectData.rotation = gameObject.transform.rotation;
        objects.Add(objectData);
        printObjects();
    }

    private void printObjects()
    {
        foreach (ObjectData objectData in objects)
        {
            Debug.Log("Object name: " + objectData.name);
            Debug.Log("Object position: " + objectData.position);
            Debug.Log("Object rotation: " + objectData.rotation);
        }
    }
}