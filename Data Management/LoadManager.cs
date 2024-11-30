using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// attach to load manager empty object in main menu scene
public class LoadManager : MonoBehaviour
{
    private static LoadManager instance;
    public List<GameObject> prefabs;
    private Dictionary<string, GameObject> prefabDict;

    // make load manager a singleton
    public static LoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadManager>();
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

        // instantiate dictionary
        prefabDict = new Dictionary<string, GameObject>();

        foreach (var prefab in prefabs) 
        {
            prefabDict[prefab.name] = prefab;
        }
    }


    /*
     not working correctly because it is not finding the image target
    switched from imagetargetbehavior to gameobject since thats how its set up in the icons handler
     */
    public void Load(GameObject imageTarget)
    {
        if (imageTarget == null)
        {
            Debug.LogError("Image Target passed to LoadManager is NULL.");
            return;
        }

        List<Data> savedData = Manager.LoadData();

        bool clear = false;
        foreach  (Data data in savedData)
        {
            Debug.Log("Checking ImageTarget: " + imageTarget.name + " against saved data: " + data.imageTargetName);

            if (data.imageTargetName == imageTarget.name)
            {
                // clear first time image target is checked
                if (!clear)
                {
                    // clear any preexisting data
                    foreach (Transform child in imageTarget.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    clear = true;
                }

                Debug.Log("Loading Child");
                // instantiate object
                if (prefabDict.TryGetValue(data.prefabName, out GameObject prefab))
                {
                    Debug.Log("Prefab Found");
                    GameObject obj = Instantiate(prefab, imageTarget.transform);
                    obj.name = data.prefabName;
                    obj.transform.localPosition = data.position;
                    obj.transform.localRotation = data.rotation;
                    obj.transform.localScale = data.scale;
                }
                else
                {
                    Debug.Log("Prefab not found");
                }
            }
            else
            {
                Debug.Log("No Image Target Found");
            }
        }
    }
}
