using UnityEngine;
using Yarn.Unity;

public class _GateOpen : MonoBehaviour
{
    [YarnCommand("gateOpen")]
    public void GateOpen()
    {
        _GameController.instance.gateOpen = true;
        Destroy(this.gameObject);
    }
}
