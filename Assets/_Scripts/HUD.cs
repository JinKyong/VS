using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{
    [SerializeField] Slider expSlider;
    [SerializeField] Text levelText;
    int level;

    [SerializeField] Text killText;
    int kill;

    [SerializeField] Text timerText;
    float timer;

    private void Start()
    {
        expSlider.maxValue = 10;
        expSlider.value = 0;

        level = 1;
        levelText.text = $"Level : {level}";

        kill = 0;
        killText.text = kill.ToString();

        timer = 1800;
        timerUpdate();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timerUpdate();
    }

    public void KillEnemy()
    {
        kill++;
        killText.text = kill.ToString();
    }
    public void GetExp(int value)
    {
        float exp = expSlider.value;
        float maxExp = expSlider.maxValue;

        if (maxExp <= exp + value)
        {
            float curExp = exp + value - maxExp;

            levelUP();
            expSlider.maxValue = level * 10;
            expSlider.value = curExp;
        }
        else
            expSlider.value += value;
    }
    private void levelUP()
    {
        level++;
        levelText.text = $"LV.{level}";
    }

    private void timerUpdate()
    {
        int minute = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minute, seconds);
    }
}