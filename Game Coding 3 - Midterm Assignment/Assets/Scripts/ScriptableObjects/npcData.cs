using UnityEngine;

[CreateAssetMenu(fileName = "npcData", menuName = "NPC Profile")]
public class npcData : ScriptableObject
{
    public string npcName;
    public string startingNode;
    
    public enum dialogPhase{comeback, begin, questStarted, questCompleted, rewardGiven}
    public dialogPhase currentPhase;

    public void DialogReset()
    {
        currentPhase = dialogPhase.begin;
    }
}
