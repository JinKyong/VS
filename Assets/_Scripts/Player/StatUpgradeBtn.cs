using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeBtn : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] PlayerStat stat;
    [SerializeField] StatData data;
    [SerializeField] string keyName;

    [Space]
    [Header("Event")]
    [SerializeField] GameEvent upgradeEvent;
    [SerializeField] IntValue costValue;

    [Space]
    [Header("Component")]
    Button btn;
    [SerializeField] Image icon;
    [SerializeField] Transform levelTR;
    [SerializeField] Text costText;
    int cost;
    int sum;
    int level;

    private void Start()
    {
        btn = GetComponent<Button>();

        icon.sprite = data.icon;
        for (int i = 0; i < data.maxLevel; i++)
            levelTR.GetChild(i).gameObject.SetActive(true);

        if (PlayerPrefs.HasKey(keyName))
        {
            level = PlayerPrefs.GetInt(keyName);
            sum = 0;
            for (int i = 0; i < level; i++)
                sum += data.cost * (int)Mathf.Pow(2, i);
        }
        else
        {
            level = 0;
            sum = 0;
        }
        updateLevel();
        updateStat();
        updateCost();
    }

    public void Upgrade()
    {
        if (stat.totalCoin < cost) return;

        level++;
        sum += cost;
        PlayerPrefs.SetInt(keyName, level);

        costValue.RuntimeValue = cost;
        upgradeEvent.Raise();

        updateLevel();
        updateStat();
        updateCost();
    }
    public int ResetLevel()
    {
        if (level == 0) return 0;

        for (int i = 0; i < level; i++)
            levelTR.GetChild(i).GetChild(0).gameObject.SetActive(false);

        level = 0;
        updateStat();
        updateCost();
        PlayerPrefs.SetInt(keyName, level);
        btn.interactable = true;

        int result = sum;
        sum = 0;

        return result;
    }

    private void updateLevel()
    {
        for (int i = 0; i < level; i++)
            levelTR.GetChild(i).GetChild(0).gameObject.SetActive(true);
    }
    private void updateStat()
    {
        switch (data.type)
        {
            case StatData.EStatDataType.Health:
                stat.UHealth = (int)data.value * level;
                break;
            case StatData.EStatDataType.Damage:
                stat.UDamageRatio = data.value * level;
                break;
            case StatData.EStatDataType.AttackSpeed:
                stat.UAttackSpeedRatio = data.value * level;
                break;
            case StatData.EStatDataType.Speed:
                stat.USpeedRatio = data.value * level;
                break;
            case StatData.EStatDataType.Magent:
                stat.UMagnetRatio = data.value * level;
                break;
            default:
                break;
        }
    }
    private void updateCost()
    {
        if (level == data.maxLevel)
        {
            costText.text = "MAX";
            btn.interactable = false;
        }
        else
        {
            cost = data.cost * (int)Mathf.Pow(2, level);
            costText.text = cost.ToString();
        }
    }
}
