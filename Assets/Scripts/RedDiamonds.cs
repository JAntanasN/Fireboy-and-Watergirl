using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RedDiamonds : MonoBehaviour
{
    public TMP_Text diamondCountText;
    private int diamondCount;

    void Start()
    {
        diamondCount = GameObject.FindGameObjectsWithTag("RedDiamond").Length;
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
