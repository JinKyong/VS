using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    [SerializeField] PlayerStat statData;
    [SerializeField] IntValue upgradeCost;
    [SerializeField] Text coinText;
    List<StatUpgradeBtn> statUpgrades;

    void Start()
    {
        coinText.text = statData.totalCoin.ToString();
        statUpgrades = new List<StatUpgradeBtn>();

        foreach(Transform tr in transform)
        {
            StatUpgradeBtn btn = tr.GetComponent<StatUpgradeBtn>();
            if (btn is null) continue;

            statUpgrades.Add(btn);
        }
    }

    public void ResetUpgrade()
    {
        int coin = 0;
        foreach (var s in statUpgrades)
            coin += s.ResetLevel();

        statData.totalCoin += coin;
        coinText.text = statData.totalCoin.ToString();

        PlayerPrefs.SetInt("TotalCoin", statData.totalCoin);
        PlayerPrefs.Save();
    }
    public void UpgradeEvent()
    {
        statData.totalCoin -= upgradeCost.RuntimeValue;
        coinText.text = statData.totalCoin.ToString();

        PlayerPrefs.SetInt("TotalCoin", statData.totalCoin);
        PlayerPrefs.Save();
    }
}