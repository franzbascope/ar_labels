using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://learn.microsoft.com/en-us/dotnet/api/system.serializableattribute?view=net-8.0
[System.Serializable]
public class Data
{
    // constructor

    public string objName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public string prefabName;
    public string imageTarget;

    public Data(string name, Vector3 pos, Quaternion rot, Vector3 scl, string prefab, string imgTarget)
    {
        objName = name;
        position = pos;
        rotation = rot;
        scale = scl;
        prefabName = prefab;
        imageTarget = imgTarget;
    }
}

[System.Serializable]
public class VirtualObjectList
{
    public List<Data> objectData = new List<Data>();
}


