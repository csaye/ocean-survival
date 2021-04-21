using OceanSurvival.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OceanSurvival
{
    public class EnergyBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Attributes")]
        [SerializeField] private Color emptyColor;
        [SerializeField] private Color fullColor;

        [Header("References")]
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderFill;

        public static EnergyBar Instance;
        
        public const int MaxEnergy = 100;

        private int _energy = MaxEnergy;
        public int Energy
        {
            get { return _energy; }
            set
            {
                // Clamp energy
                _energy = Mathf.Clamp(value, 0, MaxEnergy);

                // Set energy slider to energy value
                slider.value = Energy;

                // Set slider fill color
                float t = (float)Energy / MaxEnergy;
                sliderFill.color = Operation.LerpColor(emptyColor, fullColor, t);

                // If energy zero, game over
                if (Energy == 0) GameOver();
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Initialize energy slider
            slider.maxValue = MaxEnergy;
            Energy = MaxEnergy;
        }

        public void OnPointerEnter(PointerEventData e) => Tooltip.Instance.Show($"{Energy}/{MaxEnergy} energy");
        public void OnPointerExit(PointerEventData e) => Tooltip.Instance.Hide();

        private void GameOver()
        {
            // Open game over menu
            UIManager.Instance.SetGameOver();

            // Stop time
            Time.timeScale = 0;
        }
    }
}
