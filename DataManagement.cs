using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using Vuforia;


public class DataManagement : MonoBehaviour
{
    [SerializeField] private string fileName;

    private Data data;

    private List<IDataPersistence> dataPersistenceList = new List<IDataPersistence>();

    private FileDataHandler fileDataHandler;
    public static DataManagement Instance {  get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than 1 Instance of Data Manager");
        }

        Instance = this;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceList = FindAllDataPersistenceObjects();
    }
    public void newSave()
    {
        // set default data
        this.data = new Data(
            "Default",
            Vector3.zero,
            Quaternion.identity,
            Vector3.one,
            "Cube",
            "Image Target"
            );
    }

    public void loadData()
    {
        // load save data
        this.data = fileDataHandler.Load();

        if(this.data == null)
        {
            Debug.Log("No save data found");
            newSave();
        }
        // update data elsewhere
        foreach (var obj in dataPersistenceList)
        {
            obj.LoadData(data);
        }
    }

    public void saveData()
    {
        // pass to other scripts
        foreach(var obj in dataPersistenceList)
        {
            obj.SaveData(ref data);
        }

        // save to file
        fileDataHandler.Save(data);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> list = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(list);
    }

    /*public List<GameObject> vObjects = new List<GameObject>();

    private void Start()
    {
        LoadFile();
    }

    public void SaveFile()
    {
        string path = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(path)) 
            file = File.OpenWrite(path);
        else
            file = File.Create(path);

        VirtualObjectList dataList = new  VirtualObjectList();

        foreach (var obj in vObjects)
        {
            if(obj != null)
            {
                string imageTargetName = obj.transform.parent != null ? obj.transform.parent.name : "";
                Data data = new Data(
                    obj.name,
                    obj.transform.position,
                    obj.transform.rotation,
                    obj.transform.localScale,
                    obj.name,
                    imageTargetName
                    );

                dataList.objectData.Add(data);

                // Log the data being saved
                Debug.Log($"Saving: {data.objName}, Position: {data.position}, Rotation: {data.rotation}, Scale: {data.scale}, Parent: {data.imageTarget}");
            }
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, dataList);
        file.Close();

        Debug.Log("Data Saved Successfully");
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/save.dat";
        FileStream file;
        if( File.Exists(path))
            file = File.OpenRead(path);
        else
        {
            // error
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        VirtualObjectList dataList = (VirtualObjectList)bf.Deserialize(file);
        file.Close();

        foreach (var obj in vObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        vObjects.Clear();

        foreach(var data in dataList.objectData)
        {
            GameObject vObject = GetPrefab(data.prefabName);
            if (vObject != null)
            {
                vObject.name = data.objName;
                vObject.transform.position = data.position;
                vObject.transform.rotation = data.rotation;
                vObject.transform.localScale = data.scale;

                ImageTargetBehaviour target = FindImageTarget(data.imageTarget);
                if (target != null)
                {
                    vObject.transform.parent = target.transform;
                }
                vObjects.Add(vObject);
                Debug.Log($"Loaded: {data.objName}, Position: {data.position}, Rotation: {data.rotation}, Scale: {data.scale}, Parent: {data.imageTarget}");
            }
        }

        Debug.Log("Data Loaded Successfully");

    }

    private GameObject GetPrefab(string name)
    {
        if(name == "Cube")
            return GameObject.CreatePrimitive(PrimitiveType.Cube);

        return null;
    }

    private ImageTargetBehaviour FindImageTarget(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            foreach (var target in FindObjectsOfType<ImageTargetBehaviour>())
            {
                if (target.name == name)
                    return target;
            }
        }

        return null;
    }*/
}
