using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleSideMenu;

public class ClickIconsHandler : MonoBehaviour
{
    public GameObject targetImage;

    public SimpleSideMenu sideMenu;
    public void showIcon(string prefabName)
    {
        Debug.Log("prefabName: " + prefabName);
        GameObject prefabIcon = Resources.Load<GameObject>(prefabName);
        if (prefabIcon != null)
        {
            GameObject instantiatedIcon = Instantiate(prefabIcon, targetImage.transform);
            instantiatedIcon.transform.SetParent(targetImage.transform);
            // set scale of the icon
            instantiatedIcon.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            BoxCollider boxCollider = instantiatedIcon.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(0.7f, 0.7f, 0.01f);
            closeMenu();
        }
    }

    private void closeMenu()
    {
        sideMenu.Close();
    }
}
