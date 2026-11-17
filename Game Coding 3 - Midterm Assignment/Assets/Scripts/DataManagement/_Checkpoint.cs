using UnityEngine;

public class _Checkpoint : MonoBehaviour, IDataHandler
{
    public bool gotCheckpoint = false;
    public Transform saveLocation;
    
    public void LoadData(_GameData data)
    {
        //no data needs to be loaded
    }

    public void SaveData(ref _GameData data)
    {
        if (gotCheckpoint)
        {
            data.playerPos = saveLocation.position;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gotCheckpoint = true;
        }
    }
}
