using TMPro;
using UnityEngine;

namespace OceanSurvival
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float secondsPerHour;
        [SerializeField] private Color dayColor, nightColor;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Light lighting;

        private float lastUpdateTime;

        // Days elapsed since start of game
        private int _days = 1;
        public int Days
        {
            get { return _days; }
            set { _days = value; }
        }

        // Hours elapsed since start of game
        private int _hours = 0;
        public int Hours
        {
            get { return _hours; }
            set
            {
                // If 24 or more hours elapsed, increment day
                if (value >= 24)
                {
                    _hours = 0;
                    Days++;
                }
                else _hours = value;

                UpdateTimeText();
                UpdateLighting();
            }
        }

        private void Awake()
        {
            lastUpdateTime = Time.time;

            UpdateTimeText();
            UpdateLighting();
        }

        private void Update()
        {
            // If seconds per hour passed
            if (Time.time - lastUpdateTime > secondsPerHour)
            {
                // Update last update time
                lastUpdateTime = Time.time;

                // Increment hours
                Hours++;
            }
        }

        // Updates time text to current day and hour
        private void UpdateTimeText()
        {
            timeText.text = $"Day {Days} Hour {Hours}";
        }

        // Linearly interpolates between two colors
        private Color LerpColor(Color a, Color b, float t) => (1 - t) * a + t * b;

        // Updates scene lighting based on current hour
        private void UpdateLighting()
        {
            float t = (float)Mathf.Abs(Hours - 12) / 12;
            lighting.color = LerpColor(dayColor, nightColor, t);
        }
    }
}
