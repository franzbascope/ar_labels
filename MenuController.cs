using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
   public void goBackToMainMenu()
   {
       UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
   }
}
