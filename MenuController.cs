using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MenuController : MonoBehaviour
{
    public SaveManager saveManager;
   public void goBackToMainMenu()
   {
    Debug.Log("Going back to main menu");
        if(saveManager == null)
        {
            saveManager = FindObjectOfType<SaveManager>();
        }
        // save data on exit
        SaveAllImageTargets();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
   }

    private void SaveAllImageTargets()
    {
        // find image targets
        ImageTargetBehaviour[] imageTargets = FindObjectsOfType<ImageTargetBehaviour>();

        if (imageTargets.Length == 0) 
        {
            Debug.LogWarning("No Image Targets to be Saved.");
            return;
        }
        
        List<Data> saveData = new List<Data>();

        // save data
        foreach (ImageTargetBehaviour target in imageTargets) 
        {
            GameObject targetObject = target.gameObject;
            saveManager.Save(targetObject);
        }

    }
}
