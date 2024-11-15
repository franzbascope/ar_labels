using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void loadPreviewScene()
    {
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
