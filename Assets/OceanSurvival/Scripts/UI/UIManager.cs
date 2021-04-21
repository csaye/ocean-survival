using UnityEngine;

namespace OceanSurvival.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject pauseCanvas;

        public static UIManager Instance;

        public bool MenuOpen { get; private set; } = false;

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
            // If escape key pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Toggle pause canvas
                MenuOpen = !MenuOpen;
                pauseCanvas.SetActive(MenuOpen);
            }
        }
    }
}
