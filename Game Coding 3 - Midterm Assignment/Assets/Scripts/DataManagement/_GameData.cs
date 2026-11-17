using UnityEngine;
using System.Collections;

[System.Serializable]
public class _GameData
{
    public Vector3 playerPos;

    //constructor for class
    //call from DataManager singleton, has default value: Vector3(0,0,0)
    public _GameData()
    {
        this.playerPos = Vector3.zero;
    }
}
