using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public void OnSettingUI()
    {
        Time.timeScale = 0f;
    }
    public void OffSettingUI()
    {
        Time.timeScale = 1f;
    }
}
