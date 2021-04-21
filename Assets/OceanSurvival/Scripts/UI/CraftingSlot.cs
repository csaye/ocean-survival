using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OceanSurvival.UI
{
    public class CraftingSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Attributes")]
        [SerializeField] private ItemCount[] input;
        [SerializeField] private ItemCount output;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image itemImage;

        private string tooltipText = "<u>Input:</u>\n";

        private void Start()
        {
            // Initialize tooltip text
            foreach (ItemCount itemCount in input) tooltipText += itemCount.ToString() + "\n";

            // Initialize count text and item image to output
            countText.text = output.count > 1 ? output.count.ToString() : "";
            itemImage.sprite = Inventory.Instance.GetItemSprite((int)output.item);
        }

        public void OnPointerEnter(PointerEventData e) => Tooltip.Instance.Show(tooltipText);
        public void OnPointerExit(PointerEventData e) => Tooltip.Instance.Hide();

        public void OnClick()
        {
            // For each input item
            foreach (ItemCount itemCount in input)
            {
                // If item not contained in inventory, return
                if (!Inventory.Instance.HasItem(itemCount)) return;
            }

            // For each input item
            foreach (ItemCount itemCount in input)
            {
                // Remove item from inventory
                if (!Inventory.Instance.RemoveItem(itemCount)) return;
            }

            // Add output item to inventory
            Inventory.Instance.AddItem(output);
        }
    }
}
