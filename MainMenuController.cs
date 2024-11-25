using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public LoadManager loadManager;
    public void loadPreviewScene()
    {
        loadManager = FindObjectOfType<LoadManager>();
        if(loadManager != null)
        {
            loadManager.Load();
        }
        else
        {
            Debug.Log("Load Manager not Found");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Preview");
    }

    public void loadCreateScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Create");
    }

    public void loadToolsScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tools");
    }
}
