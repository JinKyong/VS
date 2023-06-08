using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{
    [SerializeField] Slider expSlider;
    [SerializeField] Text levelText;
    [SerializeField] Text killText;
    int kill;

    [SerializeField] Text timerText;

    private void Start()
    {
        expSlider.maxValue = 10;
        expSlider.value = 0;

        levelText.text = $"Level : {Player.Instance.level}";

        kill = 0;
        killText.text = kill.ToString();
    }

    public void KillEnemy()
    {
        kill++;
        killText.text = kill.ToString();
    }
    public void GetExp()
    {
        expSlider.value = Player.Instance.exp;
    }
    public void LevelUP()
    {
        levelText.text = $"LV.{Player.Instance.level}";
        expSlider.maxValue = Player.Instance.MaxEXP;
    }

    public void UpdateTimer(float time)
    {
        int minute = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minute, seconds);
    }
}