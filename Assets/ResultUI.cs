using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] Image back;
    [SerializeField] GameObject board;

    [Space]
    [Header("Result")]
    [SerializeField] Sprite clearSprite;
    [SerializeField] Sprite overSprite;
    [SerializeField] Image resultImg;

    [Space]
    [Header("Value")]
    [SerializeField] IntValue killValue;
    [SerializeField] FloatValue timeValue;
    [SerializeField] Text levelText;
    [SerializeField] Text killText;
    [SerializeField] Text coinText;
    [SerializeField] Text timeText;

    public void Finish()
    {
        back.enabled = true;
        board.SetActive(true);

        resultImg.sprite = GameManager.Instance.IsClear ? clearSprite : overSprite;

        levelText.text = $"Lv\t : {Player.Instance.Stat.level}";
        killText.text = $"Kill : {killValue.RuntimeValue}";
        coinText.text = $"Coin : {Player.Instance.Stat.coin}";

        {
            if (timeValue.RuntimeValue < 0) timeValue.RuntimeValue = 0;
            float time = timeValue.IValue - timeValue.RuntimeValue;
            int minute = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timeText.text = string.Format("Time : {0:D2}:{1:D2}", minute, seconds);
        }
    }

}
