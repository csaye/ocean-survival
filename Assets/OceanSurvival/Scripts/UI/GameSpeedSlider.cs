using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OceanSurvival.UI
{
    public class GameSpeedSlider : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI modifierText;

        public void UpdateGameSpeed()
        {
            // Set game speed
            TimeManager.Instance.GameSpeedModifier = slider.value;

            // Round to two decimal places and set text
            float mod = Mathf.Round(slider.value * 100) / 100;
            modifierText.text = $"{mod}x";
        }
    }
}
