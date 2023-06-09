using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Text killText;
    [SerializeField] FloatValue timer;
    [SerializeField] IntValue kill;

    [SerializeField] Slider expSlider;
    [SerializeField] Text levelText;


    private void Start()
    {
        killText.text = kill.RuntimeValue.ToString();

        expSlider.maxValue = Player.Instance.Stat.MaxEXP;
        expSlider.value = 0;

        levelText.text = $"Level : {Player.Instance.Stat.level}";
    }
    public void UpdateTimer()
    {
        float time = timer.RuntimeValue;
        int minute = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minute, seconds);
    }
    public void KillEnemy()
    {
        kill.RuntimeValue++;
        killText.text = kill.RuntimeValue.ToString();
    }


    public void GetExp()
    {
        expSlider.value = Player.Instance.Stat.exp;
    }
    public void LevelUP()
    {
        levelText.text = $"LV.{Player.Instance.Stat.level}";
        expSlider.maxValue = Player.Instance.Stat.MaxEXP;
    }
}