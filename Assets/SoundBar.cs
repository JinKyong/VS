using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SoundBar : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] string groupName;

    void Start()
    {
        float value;
        mixer.GetFloat(groupName, out value);
        slider.value = value;

        label.text = groupName;
    }

    public void ChangeVolume()
    {
        mixer.SetFloat(groupName, slider.value);
    }
}
