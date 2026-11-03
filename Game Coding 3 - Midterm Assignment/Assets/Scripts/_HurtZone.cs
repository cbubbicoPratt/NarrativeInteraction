using UnityEngine;

public class _HurtZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<_PlayerStateMachine>().SetState(pState.hurt);
        }
    }
}
