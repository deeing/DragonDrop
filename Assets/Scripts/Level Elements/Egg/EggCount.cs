using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggCount : MonoBehaviour
{
    [SerializeField]
    private int defaultNumEggs = 10;
    [SerializeField]
    private TMP_Text numEggDisplay;

    private int numEggs;

    private void Start()
    {
        numEggs = defaultNumEggs;
        UpdateEggDisplay();
    }

    public bool HasEggs()
    {
        return numEggs > 0;
    }

    public void UseEgg()
    {
        numEggs--;
        UpdateEggDisplay();
    }

    private void UpdateEggDisplay()
    {
        numEggDisplay.text = "x" + numEggs;
    }
}
