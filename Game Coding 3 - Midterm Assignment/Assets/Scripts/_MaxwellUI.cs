using TMPro;
using UnityEngine;

public class _MaxwellUI : MonoBehaviour
{
    //want "maxwells" to display only after we have some maxwells
    public TextMeshProUGUI maxwellAmount;
    public TextMeshProUGUI maxwellLabel;

    private void Awake()
    {
        _GameController.instance.LoadMaxwellUI(this);
    }

    private void Start()
    {
        _GameController.instance.maxwellInv = this;
    }

    public void MaxwellUpdate(int maxwells)
    {
        //only show maxwells if we have any
        //this method can be called when we load a scene so that it doesnt disappear
        if(maxwells <= 0)
        {
            maxwellLabel.text = "";
            maxwellAmount.text = "";
        }
        else
        {
            maxwellLabel.text = "Maxwells:";
            maxwellAmount.text = maxwells.ToString();
        }
    }
}
