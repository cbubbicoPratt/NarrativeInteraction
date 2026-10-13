using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class _NPCInteract : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public string interactText;
    public bool canInteract;
    public DialogueRunner npcDialogue;
    public _NPC_CamRig npcCamRig;
    public npcData storyData;
    public InMemoryVariableStorage vStore;

    private void Start()
    {
        npcCamRig = transform.GetComponentInChildren<_NPC_CamRig>();
        npcDialogue = FindAnyObjectByType<DialogueRunner>();
        vStore = FindAnyObjectByType<InMemoryVariableStorage>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetText(interactText);
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
        promptText.text = text;
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Starting npc dialogue: " + storyData.npcName);
                SetText("");
                npcCamRig.MovePlayer();
                npcDialogue.StartDialogue(storyData.startingNode);
                canInteract = false;
            }
        }
    }

    [YarnCommand("setPhase")]
    public void SetDialogPhase()
    {
        //InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage
        vStore.SetValue("$dialoguePhase_" + storyData.npcName, storyData.currentPhase.ToString());
        Debug.Log(storyData.currentPhase.ToString());
    }

    [YarnCommand("getPhase")]
    public void GetDialogPhase(string phase)
    {
        Debug.Log(phase);
        if (phase == "comeback")
        {
            storyData.currentPhase = npcData.dialogPhase.comeback;
        }
        else if (phase == "questStarted")
        {
            storyData.currentPhase = npcData.dialogPhase.questStarted;
        }
        else if (phase == "questCompleted")
        {
            storyData.currentPhase = npcData.dialogPhase.questCompleted;
        }
        else if (phase == "rewardGiven")
        {
            storyData.currentPhase = npcData.dialogPhase.rewardGiven;
        }
        else
        {
            Debug.LogError("String passed in YarnScript does not match any current dialogue phase in npcData scriptableObject");
        }
    }

    [YarnCommand("setMaxwells")]
    public void SetMaxwells()
    {
        //InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        vStore.SetValue("$totalMaxwells" + storyData.npcName, _GameController.instance.totalMaxwells);
    }

    [YarnCommand("checkGate")]
    public void CheckGate()
    {
        //InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        if (FindAnyObjectByType<_GateOpen>() == null)
        {
            vStore.SetValue("$gateOpen", true);
        }
        else
        {
            vStore.SetValue("$gateOpen", false);
        }
    }

    public void OnApplicationQuit()
    {
        storyData.DialogReset();
    }
}
