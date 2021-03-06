using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OceanSurvival.UI
{
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Attributes")]
        [SerializeField] private int slotIndex;
        [SerializeField] private Color normalColor, selectedColor;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image slotImage, itemImage;
        [SerializeField] private Sprite transparent;

        // Item count
        private int _count = 0;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                if (Count == 0) Item = 0;

                // Update count text
                countText.text = Count > 1 ? Count.ToString() : "";
            }
        }
        
        // Item
        private Item _item = 0;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                // Update item sprite
                itemImage.sprite = Item == 0 ? transparent : Inventory.Instance.GetItemSprite(Item);
            }
        }

        // Whether slot is empty
        public bool IsEmpty => Count == 0 || Item == 0;

        // Sets item ID and count to given values
        public void SetSlot(Item item, int count)
        {
            Item = item;
            Count = count;
        }

        public void OnPointerEnter(PointerEventData e)
        {
            if (!IsEmpty)
            {
                string tooltipText = $"{Count} {Item}";
                if (Item == Item.Seaweed) tooltipText += "\n5 energy";
                else if (Item == Item.Fish) tooltipText += "\n15 energy";
                Tooltip.Instance.Show(tooltipText);
            }
        }
        public void OnPointerExit(PointerEventData e) => Tooltip.Instance.Hide();

        // Selects current slot
        public void OnClick() => Inventory.Instance.SelectSlot(slotIndex);

        // Sets slot color based on whether selected
        public void SetSelected(bool selected) => slotImage.color = selected ? selectedColor : normalColor;
    }
}
