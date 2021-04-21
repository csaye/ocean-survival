using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OceanSurvival.UI
{
    public class CraftingSlot : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private ItemCount[] input;
        [SerializeField] private ItemCount output;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image itemImage;

        private void Start()
        {
            // Initialize count text and item image to output
            countText.text = output.count > 1 ? output.count.ToString() : "";
            itemImage.sprite = Inventory.Instance.GetItemSprite((int)output.item);
        }

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
