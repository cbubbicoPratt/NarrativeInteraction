using TMPro;
using UnityEngine;
using Yarn.Compiler;

public class _LockedObject : MonoBehaviour, iKeyHandler
{
    public bool canBeUnlocked;
    public bool canInteract;
    public TextMeshProUGUI interactPrompt;
    
    public void CanUnlock()
    {
        canBeUnlocked = true;
    }

    public void RemoveKey()
    {
        canBeUnlocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(canBeUnlocked)
            {
                setText("Press E to unlock");
                canInteract = true;
            } 
            else
            {
                setText("Requires a Key to unlock");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            setText("");
        }
    }

    public void setText(string txt)
    {
        interactPrompt.text = txt;
    }
}
