using UnityEngine;

public enum pState {OK, hurt};

public class _PlayerStateMachine : MonoBehaviour
{
    public pState playerState;

    //below is setter method which takes enum as arg and sets current player state
    public void SetState(pState state)
    {
        playerState = state;
    }
}
