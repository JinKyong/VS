using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    private void Start()
    {
        hpSlider.maxValue = Player.Instance.health;
        hpSlider.value = hpSlider.maxValue;
    }

    public void UpdateHP()
    {
        hpSlider.value = Player.Instance.health;
    }
}
