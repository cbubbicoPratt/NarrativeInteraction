using System.Collections.Generic;
using UnityEngine;

public class _DataManager : MonoBehaviour
{
    public static _DataManager instance { get; private set; }
    private List<IDataHandler> dataObjs = new List<IDataHandler>();
    private _GameData gameData;
    private _FileDataManager fileManager;

    [SerializeField] private string fileName;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        this.fileManager = new _FileDataManager(Application.persistentDataPath, fileName);
        GetAllObjects();
        LoadGame();
    }

    public void GetAllObjects()
    {
        MonoBehaviour[] allScripts = FindObjectsByType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IDataHandler)
            {
                dataObjs.Add(allScripts[i] as IDataHandler);
            }
        }
    }

    public void NewGame()
    {
        //using _GameData class constructor to make sure data is stored in variable before overwriting data to it
        this.gameData = new _GameData();
    }

    public void LoadGame()
    {
        //FINISHED: use file writing system to deserialize stored game data on computer and load into this class
        this.gameData = fileManager.LoadFile();
        
        if (this.gameData == null)
        {
            Debug.Log("No game data found. Loaded default data.");
            NewGame();
        }
        
        Debug.Log("Loaded player position: " + gameData.playerPos.ToString());
        
        //FINISHED: take _GameData variable in class and "push" to all interface instances
        foreach (IDataHandler dataHandler in dataObjs)
        {
            dataHandler.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //FINISHED: get all _GameData from interface instances + store in local gameData variable
        foreach(IDataHandler dataHandler in dataObjs)
        {
            dataHandler.SaveData(ref gameData);
        }
        
        Debug.Log("Saved player position: " + gameData.playerPos.ToString());
        
        //FINISHED: use file writing system to serialize gameData variable into file on my computer
        fileManager.SaveFile(gameData);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }
}
