using UnityEngine;

namespace OceanSurvival.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private GameObject gameOverCanvas;

        public static UIManager Instance;

        public bool MenuOpen { get; private set; } = false;
        public bool GameOver { get; private set; } = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            pauseCanvas.SetActive(false);
        }

        private void Update()
        {
            // If escape key pressed and not game over
            if (Input.GetKeyDown(KeyCode.Escape) && !GameOver)
            {
                // Toggle menu open
                SetMenuOpen(!MenuOpen);
            }
        }

        public void SetMenuOpen(bool menuOpen)
        {
            // Toggle pause canvas
            MenuOpen = menuOpen;
            if (!menuOpen) Tooltip.Instance.Hide();
            pauseCanvas.SetActive(menuOpen);
        }

        public void SetGameOver()
        {
            // Show game over menu
            GameOver = true;
            SetMenuOpen(false);
            gameOverCanvas.SetActive(true);
        }
    }
}
