using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// create UI manager in each scene to allow saving & loading through buttons
public class UI : MonoBehaviour
{

    public static UI instance;

    public SaveManager GlobalSaveManager;
    public LoadManager GlobalLoadManager;

    private const string createScene = "Create";
    private const string mainMenu = "Main";
    private const string previewScene = "Preview";
    private const string toolsScene = "Tools";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        if (GlobalSaveManager == null)
        {
            GlobalSaveManager = FindObjectOfType<SaveManager>();
            if (GlobalSaveManager != null)
                Debug.Log("Found SaveManager");
            else
                Debug.Log("Did Not Find SaveManager");
        }
        if (GlobalLoadManager == null)
        {
            GlobalLoadManager = FindObjectOfType<LoadManager>();
        }
        DontDestroyOnLoad(this);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == createScene)
        {
            Debug.Log("Entering Create");
        }
        else if (scene.name == previewScene)
        {
            Debug.Log("Entering Preview");
        }
        else
        {
            // main menu
            Debug.Log("Returning to Main Menu");
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == createScene)
        {
            Debug.Log("Leaving Create");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
