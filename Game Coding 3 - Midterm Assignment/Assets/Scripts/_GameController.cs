using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class _GameController : MonoBehaviour
{
    //GameController needs to handle:
    //Collectibles
    //Player movement/camera movement during npc interaction
    //Player spawning (n/a because we don't have multiple scenes but good practice + prevents bugs)
    
    //Accessible in any class, cannot be changed
    public static _GameController instance { get; private set; }
    
    public DialogueRunner dialogueRunner;
    
    public _AddMaxwells maxwellUpdate;
    public int totalMaxwells;
    public _MaxwellUI maxwellInv;

    public List<string> maxwellsCollected;
    
    public GameObject playerRig;
    public Transform startPos;
    public Vector3 loadPos;
    public Quaternion loadRot;

    public bool gateOpen;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoad;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(dialogueRunner.gameObject);
        }
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (loadPos == Vector3.zero)
            {
                SpawnPlayer(startPos.position, startPos.rotation);
            }
            else
            {
                SpawnPlayer(loadPos, loadRot);
            }
            if (gateOpen)
            {
                Destroy(FindAnyObjectByType<_GateOpen>());
            }
        }
        maxwellUpdate.AddListener(maxwellInv.MaxwellUpdate);
        LoadMaxwells();
        maxwellInv.MaxwellUpdate(totalMaxwells);
    }

    public void LoadLocationData(Vector3 pos, Quaternion rot)
    {
        loadPos = pos;
        loadRot = rot;
    }

    public void SpawnPlayer(Vector3 pos, Quaternion rot)
    {
        Instantiate(playerRig, pos, rot);
    }

    public void SetPlayerMovement(bool b)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ThirdPersonController tpc = player.GetComponent<ThirdPersonController>();
        if (b)
        {
            Cursor.lockState = CursorLockMode.None;
            tpc.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            tpc.enabled = true;
        }
    }

    public void MovePlayerOnNPC(Transform newPos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = newPos.position;
        player.transform.rotation = newPos.rotation;
    }

    public void MaxwellCollect(string maxwellID)
    {
        //add maxwell's id to a list of collected maxwells
        //then add to the current total of collected maxwells
        //finally, broadcast an event with the total so we can update UI
        maxwellsCollected.Add(maxwellID);
        totalMaxwells++;
        maxwellUpdate.Invoke(totalMaxwells);
    }

    [YarnCommand("collectMaxwell")]
    public void NPCMaxwellCollect()
    {
        totalMaxwells++;
        maxwellUpdate.Invoke(totalMaxwells);
    }

    public void LoadMaxwells()
    {
        //when we load the scene we want to get rid of every maxwell we collected
        //first we find all maxwells in the scene and put them on a list
        _MaxwellCollect[] allSceneMaxwells = FindObjectsByType<_MaxwellCollect>();
        
        //if we have collected maxwells we compare their ids to the ids of ones present in scene
        //if they match, we already collected it and it can be destroyed
        if (maxwellsCollected != null)
        {
            foreach (_MaxwellCollect maxwell in allSceneMaxwells)
            {
                if (maxwellsCollected.Contains(maxwell.maxwellID))
                {
                    Destroy(maxwell.gameObject);
                }
            }
        }
    }

    //set maxwellUI for scene
    public void LoadMaxwellUI(_MaxwellUI maxwellUI)
    {
        maxwellInv = maxwellUI;
    }
}

//blank class to handle updating maxwells
[System.Serializable]
public class _AddMaxwells : UnityEvent<int>
{
    
}
