using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Text healthText;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateSliderValue(int currentHP, int maxHP)
    {
        slider.value = (float) currentHP / maxHP;
        healthText.text = "HP: " + currentHP; 
    }
}
