using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OceanSurvival.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private int slotIndex;
        [SerializeField] private Color normalColor, selectedColor;

        [Header("References")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image itemImage;
        [SerializeField] private Sprite transparent;

        // Item count
        private int _count = 0;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;

                // Update count text
                countText.text = Count == 0 ? "" : Count.ToString();
            }
        }
        
        // Item ID
        private int _itemID = 0;
        public int ItemID
        {
            get { return _itemID; }
            set
            {
                _itemID = value;

                // Update item sprite
                itemImage.sprite = ItemID == 0 ? transparent : Inventory.Instance.GetItemSprite(ItemID);
            }
        }

        // Whether slot is empty
        public bool IsEmpty => Count == 0 || ItemID == 0;

        // Sets item ID and count to given values
        public void SetSlot(int itemID, int count)
        {
            ItemID = itemID;
            Count = count;
        }

        // Selects current slot
        public void ClickSlot() => Inventory.Instance.SelectSlot(slotIndex);

        // Sets slot color based on whether selected
        public void SetSelected(bool selected) => slotImage.color = selected ? selectedColor : normalColor;
    }
}
