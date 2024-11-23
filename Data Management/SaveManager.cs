using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to save manager empty object in main menu
public class SaveManager : MonoBehaviour
{
    
    public List<Data> imageTargetList = new List<Data>();

    // dont destroy on load needed to save scene on exit, moving from main menu removes managers from initial start Awake
    private void Awake()
    {
        if(FindObjectsOfType<SaveManager>().Length > 1)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void Save()
    {
        Debug.Log("Save is Starting");

        imageTargetList.Clear();

        // get image targets 
        var imageTargets = FindObjectsOfType<Vuforia.ImageTargetBehaviour>();
        Debug.Log($"Found {imageTargets.Length} image targets");

        // iterate through each image target & add to list
        foreach (var imageTarget in imageTargets)
        {
            Debug.Log("Adding Image Target");
            Data data = new Data
            {
                imageTargetName = imageTarget.name,
                imageTargetPosition = imageTarget.transform.position,
                imageTargetRotation = imageTarget.transform.rotation,
                imageTargetScale = imageTarget.transform.localScale,
                listOfObjects = new List<objectData>()
            };

            // iterate through each object in image target
            foreach(Transform child in imageTarget.transform)
            {
                Debug.Log("Adding Object");
                objectData objData = new objectData
                {
                    prefabName = child.name,
                    position = child.position,
                    rotation = child.rotation,
                    scale = child.localScale
                };

                data.listOfObjects.Add(objData);
            }

            // add to save
            Debug.Log("Data added to save");
            imageTargetList.Add(data);
        }

        Debug.Log("Save complete");
        // save data
        Datamanager.saveData(imageTargetList);
    }
}
