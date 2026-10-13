using UnityEngine;
using UnityEngine.SceneManagement;

public class _TunnelEnter : MonoBehaviour
{
    public Transform tunnelReturn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _GameController.instance.LoadLocationData(tunnelReturn.position, tunnelReturn.rotation);

            SceneManager.LoadScene(1);
        }
    }
}
