using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to load manager empty object in main menu scene
public class LoadManager : MonoBehaviour
{
    public List<Data> imageTargetData;
    public Transform scene;
    
    // create folder or manually assign them from inspector
    public List<GameObject> prefabs;

    // dont destroy on load needed to load scene on enter, moving from main menu removes managers from initial start Awake
    private void Awake()
    {
        if (FindObjectsOfType<LoadManager>().Length > 1)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }
    public void Load()
    {
        Debug.Log("Beginning Load");
        // load data 
        imageTargetData = Datamanager.LoadData();

        if(imageTargetData == null || imageTargetData.Count == 0)
        {
            Debug.Log("No Image Target Data Found");
        }
        else
        {
            Debug.Log($"Loaded {imageTargetData.Count} image target(s) from data.");
        }

        foreach (var data in imageTargetData)
        {
            Debug.Log($"Processing image target: {data.imageTargetName}");
            // get image target
            var imageTarget = FindImageTarget(data.imageTargetName);
            if (imageTarget == null)
            {
                Debug.Log("Image Target Not Found");
            }
            Debug.Log("Image Target Obtained");
            imageTarget.transform.position = data.imageTargetPosition;
            Debug.Log("Position Obtained");
            imageTarget.transform.rotation = data.imageTargetRotation;
            Debug.Log("Rotation Obtained");
            imageTarget.transform.localScale = data.imageTargetScale;
            Debug.Log("Scale Obtained");

            // if null go to next object
            if (imageTarget == null)
                continue;

            foreach (var objectData in data.listOfObjects)
            {
                Debug.Log($"Processing object: {objectData.prefabName}");

                GameObject prefab = prefabs.Find(p => p.name == objectData.prefabName);
                
                // if null go to next object
                if (prefab == null)
                    continue;

                GameObject obj = Instantiate(prefab, imageTarget.transform);
                obj.transform.localPosition = objectData.position;
                obj.transform.localRotation = objectData.rotation;
                obj.transform.localScale = objectData.scale;
            }
        }
        Debug.Log("Load Complete");

    }
    private Vuforia.ImageTargetBehaviour FindImageTarget(string name)
    {
        var imageTargets = FindObjectsOfType<Vuforia.ImageTargetBehaviour>();
        foreach (var target in imageTargets)
        {
            if (target.name == name)
                return target;
        }

        return null;
    }
}

