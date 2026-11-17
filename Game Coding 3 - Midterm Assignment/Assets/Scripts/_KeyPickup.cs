using UnityEngine;

public class _KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _KeyInventoryManager.instance.CollectedKey();
            ToggleObj(false);
        }
    }

    public void ToggleObj(bool state)
    {
        GetComponent<MeshRenderer>().enabled = state;
        GetComponent<MeshCollider>().enabled = state;
        GetComponent<SphereCollider>().enabled = state;
    }
}
