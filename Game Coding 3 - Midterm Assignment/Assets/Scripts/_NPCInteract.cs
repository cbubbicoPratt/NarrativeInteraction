using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public enum iType {npc, item}

public class _NPCInteract : MonoBehaviour
{
    [Tooltip("Select the type of interaction to gain access to necessary variables.")]
    public iType type;
    
    [System.Serializable]
    public struct TextData
    {
        public TextMeshProUGUI promptText;
        public string interactText;
    }
    [Header("Text Data Variables")]
    public TextData textData;
    
    public bool canInteract;

    [System.Serializable]
    public struct DialogueData
    {
        public DialogueRunner npcDialogue;
        public npcData storyData;
        public InMemoryVariableStorage vStore;
        public _NPCStateCheck stateCheck;
    }

    [Header("Story & YarnSpinner Variables")] 
    public DialogueData dialogueData;
    
    [System.Serializable]
    public struct CamData
    {
        public GameObject npcCamPrefab;
        public _NPC_CamRig npcCamRig;
    }
    [Header("NPC Cam Rig Variables")]
    public CamData camData;
    
    //---------EDITOR METHODS---------//

    public void LoadText()
    {
        textData.promptText = GameObject.FindGameObjectWithTag("pText").GetComponent<TextMeshProUGUI>();
    }

    public void LoadNpcCamPrefab()
    {
        Instantiate(camData.npcCamPrefab, transform);
        camData.npcCamRig = transform.GetComponentInChildren<_NPC_CamRig>();
    }

    public void LoadYS()
    {
        dialogueData.npcDialogue = GameObject.FindGameObjectWithTag("DialogueRunner").GetComponent<DialogueRunner>();
        dialogueData.vStore = dialogueData.npcDialogue.GetComponent<InMemoryVariableStorage>();
    }
    //---------RUNTIME METHODS---------//
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetText(textData.interactText);
            canInteract = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetText("");
            canInteract = false;
        }
    }
    
    public void SetText(string text)
    {
        textData.promptText.text = text;
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Starting npc dialogue: " + dialogueData.storyData.npcName);
                SetText("");
                pState currentState = GameObject.FindGameObjectWithTag("Player").GetComponent<_PlayerStateMachine>().playerState;
                string startingNodeName = dialogueData.stateCheck.CheckState(currentState);
                dialogueData.npcDialogue.StartDialogue(startingNodeName);
                //camData.npcCamRig.MovePlayer();
                //dialogueData.npcDialogue.StartDialogue(dialogueData.storyData.startingNode);
                canInteract = false;
            }
        }
    }

    [YarnCommand("setPhase")]
    public void SetDialogPhase()
    {
        //InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage
        dialogueData.vStore.SetValue("$dialoguePhase_" + dialogueData.storyData.npcName, dialogueData.storyData.currentPhase.ToString());
        Debug.Log(dialogueData.storyData.currentPhase.ToString());
    }

    [YarnCommand("getPhase")]
    public void GetDialogPhase(string phase)
    {
        Debug.Log(phase);
        if (phase == "comeback")
        {
            dialogueData.storyData.currentPhase = npcData.dialogPhase.comeback;
        }
        else if (phase == "questStarted")
        {
            dialogueData.storyData.currentPhase = npcData.dialogPhase.questStarted;
        }
        else if (phase == "questCompleted")
        {
            dialogueData.storyData.currentPhase = npcData.dialogPhase.questCompleted;
        }
        else if (phase == "rewardGiven")
        {
            dialogueData.storyData.currentPhase = npcData.dialogPhase.rewardGiven;
        }
        else
        {
            Debug.LogError("String passed in YarnScript does not match any current dialogue phase in npcData scriptableObject");
        }
    }
    
    // [YarnCommand("setMaxwells")]
    // public void SetMaxwells()
    // {
    //     //InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
    //     vStore.SetValue("$totalMaxwells" + storyData.npcName, _GameController.instance.totalMaxwells);
    // }

    // [YarnCommand("checkGate")]
    // public void CheckGate()
    // {
    //     //InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
    //     if (FindAnyObjectByType<_GateOpen>() == null)
    //     {
    //         vStore.SetValue("$gateOpen", true);
    //     }
    //     else
    //     {
    //         vStore.SetValue("$gateOpen", false);
    //     }
    //}

    public void OnApplicationQuit()
    {
        dialogueData.storyData.DialogReset();
    }
}
