using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class for image targets
[Serializable]
public class Data
{
    public string imageTargetName;
    public Vector3 imageTargetPosition;
    public Quaternion imageTargetRotation;
    public Vector3 imageTargetScale;
    public List<objectData> listOfObjects;
}

// class for objects within image targets
[Serializable]
public class objectData
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}
