using TMPro;
using UnityEngine;

namespace OceanSurvival.UI
{
    public class Tooltip : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI panelText;

        public static Tooltip Instance;

        private readonly Vector3 Offset = new Vector3(10, -10, 0);

        private void Awake()
        {
            Instance = this;
        }

        public void Show(string text)
        {
            panelText.text = text;
            panel.SetActive(true);
            panel.transform.position = Input.mousePosition + Offset;
        }

        public void Hide()
        {
            panel.SetActive(false);
        }
    }
}
