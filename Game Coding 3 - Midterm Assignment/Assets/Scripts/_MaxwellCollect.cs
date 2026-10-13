using UnityEngine;

public class _MaxwellCollect : MonoBehaviour
{
    //id for differentiating maxwells
    public string maxwellID;

    private void Awake()
    {
        maxwellID = this.name + "_" + transform.position.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        //add maxwell to a list then "disable" him
        _GameController.instance.MaxwellCollect(maxwellID);
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
