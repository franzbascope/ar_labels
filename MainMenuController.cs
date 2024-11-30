using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class MainMenuController : MonoBehaviour
{
    public LoadManager loadManager;

    public void loadPreviewScene()
    {

        SceneManager.sceneLoaded += OnPreviewSceneLoaded;
        SceneManager.LoadScene("Preview");
    }

    private void OnPreviewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Preview")
        {
            SceneManager.sceneLoaded -= OnPreviewSceneLoaded; 

            // Find image targets and load data
            ImageTargetBehaviour[] imageTargets = FindObjectsOfType<ImageTargetBehaviour>();

            if (imageTargets.Length > 0)
            {
                Debug.Log("Image Targets Found");
                foreach (ImageTargetBehaviour imageTarget in imageTargets)
                {
                    GameObject targetObject = imageTarget.gameObject;
                    if(targetObject == null)
                    {
                        Debug.Log("TargetObject is Null");
                        continue;
                    }
                    Debug.Log("Loading Object");
                    loadManager.Load(targetObject);
                }
            }
            else
            {
                Debug.Log("No Image Targets Found");
            }
        }
    }

    public void loadCreateScene()
    {
        SceneManager.LoadScene("Create");
    }

    public void loadToolsScene()
    {
        SceneManager.LoadScene("Tools");
    }
}
