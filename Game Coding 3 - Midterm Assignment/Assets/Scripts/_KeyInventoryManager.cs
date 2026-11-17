using UnityEngine;
using System.Collections.Generic;

public class _KeyInventoryManager : MonoBehaviour
{
    public static _KeyInventoryManager instance { get; private set;}
    public List<IKeyHandler> allLockedObjects = new List<IKeyHandler>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LoadAllLockedObjects();
    }

    //meethod loads all iKeyHandler scripts and stores them into allLockedObjects List
    //retrieves all MonoBehaviour "objects" in our scene, separates out iKeyHandler interfaces
    public void LoadAllLockedObjects()
    {
        MonoBehaviour[] allScripts = FindObjectsByType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IKeyHandler)
            {
                allLockedObjects.Add(allScripts[i] as IKeyHandler);
            }
        }
    }

    //below method goes through each iKeyHandler interface "instance"
    //calls CanUnlock() method
    //allLockedObjects contains all of the scripts in our scene which use iKeyHandler interface
    public void CollectedKey()
    {
        foreach (IKeyHandler keyHandler in allLockedObjects)
        {
            keyHandler.CanUnlock();
        }
    }

    //below method goes through each iKeyHandler interface "instance"
    //calls RemoveKey() method
    public void UsedKey()
    {
        foreach (IKeyHandler keyHandler in allLockedObjects)
        {
            keyHandler.RemoveKey();
        }
    }
}
