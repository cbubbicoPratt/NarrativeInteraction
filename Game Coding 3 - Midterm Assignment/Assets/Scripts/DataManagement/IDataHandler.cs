using UnityEngine;

public interface IDataHandler
{
    public void LoadData(_GameData data);
    public void SaveData(ref _GameData data);
}
