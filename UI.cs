using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public DataManagement dataManager;
    public ModifyObject ModifyObject;

    public void onSaveButtonClicked()
    {
        dataManager.saveData();
    }

    public void onLoadButtonClicked()
    {
        dataManager.loadData();
    }

    public void onCreateButtonClicked()
    {
        ModifyObject.createObject();
    }

    public void onDeleteButtonClicked()
    {
        ModifyObject.deleteObject();
    }

    public void onEditButtonClicked()
    {
        ModifyObject.editObject();
    }
}
