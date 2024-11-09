using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataManagement : MonoBehaviour
{
    public List<GameObject> vObjects = new List<GameObject>();
    private string savePath;

    private void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
        savePath = Path.Combine(Application.persistentDataPath, "VirtualObjectsData.json");
        loadData();
    }

    public void saveData()
    {
        VirtualObjectList dataList = new VirtualObjectList();

        foreach (var obj in vObjects)
        {
            if (obj != null)
            {
                Data data = new Data(
                    obj.name,
                    obj.transform.position,
                    obj.transform.rotation,
                    obj.transform.localScale
                    );

                dataList.objectData.Add(data);
            }
        }

        // https://docs.unity3d.com/ScriptReference/JsonUtility.ToJson.html
        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(savePath, json);

    }

    public void loadData()
    {
        if (!File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            // https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
            VirtualObjectList dataList = JsonUtility.FromJson<VirtualObjectList>(json);

            // clear
            foreach (var obj in vObjects)
            {
                if (obj != null)
                    Destroy(obj);
            }
            vObjects.Clear();


            foreach (var data in dataList.objectData)
            {
                GameObject virtualObject = Instantiate(data.model);
                if (virtualObject != null)
                {
                    virtualObject.name = data.objName;
                    virtualObject.transform.position = data.position;
                    virtualObject.transform.rotation = data.rotation;
                    virtualObject.transform.localScale = data.scale;

                    vObjects.Add(virtualObject);
                }
            }
        }

        else
        {
            Debug.LogWarning("File not Found");
        }
    }
}
