using TMPro;
using UnityEngine;

namespace OceanSurvival
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float secondsPerHour;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI timeText;

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
            }
        }

        private void Awake()
        {
            lastUpdateTime = Time.time;
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
    }
}
