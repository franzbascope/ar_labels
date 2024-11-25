using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public SaveManager saveManager;
   public void goBackToMainMenu()
   {
        saveManager = FindObjectOfType<SaveManager>();

        if(saveManager != null)
        {
            saveManager.Save();
        }
        else
        {
            Debug.Log("No Save Manager Found");
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
   }
}
