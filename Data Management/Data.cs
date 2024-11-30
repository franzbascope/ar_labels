using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class for image targets
[Serializable]
public class Data
{
    public string imageTargetName;
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Data(string imageTargetName, string prefabName, Transform transform)
    {
        this.imageTargetName = imageTargetName;
        this.prefabName = prefabName;
        position = transform.localPosition;
        rotation = transform.localRotation;
        scale = transform.localScale;
    }
}

