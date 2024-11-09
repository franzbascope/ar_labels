using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyObject : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> vObjects = new List<GameObject>();
    public Selection selectionScript;
    public InputField nameInput;

    public void createObject()
    {
        GameObject newObject = Instantiate(prefab);
        string objectName;
        if (nameInput != null && !string.IsNullOrEmpty(nameInput.text))
            objectName = nameInput.text;
        else
            objectName = "Object" + vObjects.Count; 

        newObject.name = objectName;
        newObject.transform.position = new Vector3(0, 0, 0);
        newObject.transform.rotation = Quaternion.identity;
        newObject.transform.localScale = Vector3.one;

        vObjects.Add(newObject);

        if (selectionScript != null)
            selectionScript.createObjectSelect(newObject);
    }

    public void deleteObject()
    {
        if(selectionScript != null && selectionScript.isAnObjectSelected)
        {
            GameObject selectedObject = selectionScript.selectedObject;
            if (selectedObject != null)
            {
                vObjects.Remove(selectedObject);
                Destroy(selectedObject);
                selectionScript.forceDeselect();
            }
        }
        else
        {
            Debug.LogWarning("No Object Selected");
        }
    }

    public void editObject()
    {

    }
}
