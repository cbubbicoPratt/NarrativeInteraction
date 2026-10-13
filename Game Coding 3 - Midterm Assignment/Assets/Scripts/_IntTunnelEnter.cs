using System;
using UnityEngine;

public class _IntTunnelEnter : MonoBehaviour
{
    private void Start()
    {
        _GameController.instance.SpawnPlayer(this.transform.position, this.transform.rotation);
    }
}
