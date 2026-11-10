using UnityEngine;

public class _KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _KeyInventoryManager.instance.CollectedKey();
        }
    }
}
