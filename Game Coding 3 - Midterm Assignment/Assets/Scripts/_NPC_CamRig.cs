using Yarn.Unity;
using Unity.Cinemachine;
using UnityEngine;

public class _NPC_CamRig : MonoBehaviour
{
    public Transform npcLoc;
    public Transform pcLoc;
    public Transform pcMoveLoc;
    public int pNum;
    public CinemachineCamera npc_cCam;

    [YarnCommand("camMove")]
    public void CamMove(string camName)
    {
        npc_cCam.Priority = pNum;
        if (camName == "npc")
        {
            npc_cCam.gameObject.transform.position = npcLoc.position;
        }
        else if (camName == "pc")
        {
            npc_cCam.gameObject.transform.position = pcLoc.position;
        }
        else if (camName == "return")
        {
            npc_cCam.Priority = 0;
        }
    }
    
    public void MovePlayer()
    {
        _GameController.instance.MovePlayerOnNPC(pcMoveLoc);
    }
}
