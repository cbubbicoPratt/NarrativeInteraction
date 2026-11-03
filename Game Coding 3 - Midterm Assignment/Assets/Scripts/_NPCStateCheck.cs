using UnityEngine;

public class _NPCStateCheck : MonoBehaviour
{
    //below method uses 'string' as return value
    //all methods can use data type but MUST return that value
    public string CheckState(pState playerState)
    {
        string currentPlayerState = "";

        //'switch' is a type of logic that easily replaces if/else blocks
        //use 'case' to see if arg in switch is met
        //all cases need a 'break' to operate as intended
        switch (playerState)
        {
            case pState.OK:
                //return string value passed into dialogue runner (Yarn Spinner) class
                currentPlayerState = "beginOK";
                break;
            case pState.hurt:
                currentPlayerState = "beginHurt";
                break;
        }
        
        return currentPlayerState;
    }
}
