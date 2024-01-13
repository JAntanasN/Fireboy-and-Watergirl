using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueDiamonds : MonoBehaviour
{
    public TMP_Text diamondCountText;
    private int diamondCount;

    void Start()
    {
        diamondCount = GameObject.FindGameObjectsWithTag("BlueDiamond").Length;
        UpdateDiamondCountUI();
    }

    public void CollectDiamond()
    {
        diamondCount--;
        UpdateDiamondCountUI();
    }

    private void UpdateDiamondCountUI()
    {
        diamondCountText.text = "Diamonds: " + diamondCount;
    }
}
