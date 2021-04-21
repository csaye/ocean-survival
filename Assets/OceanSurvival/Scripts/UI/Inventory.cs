using UnityEngine;

namespace OceanSurvival.UI
{
    public class Inventory : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private Sprite[] itemSprites;

        [Header("References")]
        [SerializeField] private InventorySlot[] inventorySlots;

        private readonly KeyCode[] HotbarKeys = new KeyCode[]
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
            KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0
        };
        
        public static Inventory Instance;

        private int selectedSlotIndex = 0;

        public Sprite GetItemSprite(Item item) => GetItemSprite((int)item);
        public Sprite GetItemSprite(int itemID) => itemSprites[itemID];

        public InventorySlot SelectedSlot => inventorySlots[selectedSlotIndex];

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SelectSlot(selectedSlotIndex);
        }

        // Adds given item to inventory
        public void AddItem(ItemCount itemCount) => AddItem(itemCount.item, itemCount.count);
        public void AddItem(Item item, int count)
        {
            // For each inventory slot
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // If stackable to position, stack
                if (inventorySlots[i].Item == item)
                {
                    inventorySlots[i].Count += count;
                    return;
                }
            }

            // For each inventory slot
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // If item at position empty, set item
                if (inventorySlots[i].IsEmpty)
                {
                    inventorySlots[i].SetSlot(item, count);
                    return;
                }
            }
        }

        // Returns whether inventory currently has item
        public bool HasItem(ItemCount itemCount) => HasItem(itemCount.item, itemCount.count);
        public bool HasItem(Item item, int count)
        {
            // For each inventory slot
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // Get slot at inventory position
                InventorySlot slot = inventorySlots[i];

                // If slot has item, return true
                if (slot.Item == item && slot.Count >= count) return true;
            }

            // If no slots have item, return false
            return false;
        }

        // Removes given item from inventory and returns whether successful
        public bool RemoveItem(ItemCount itemCount) => RemoveItem(itemCount.item, itemCount.count);
        public bool RemoveItem(Item item, int count)
        {
            // For each inventory slot
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // Get slot at inventory position
                InventorySlot slot = inventorySlots[i];

                // If slot has item
                if (slot.Item == item && slot.Count >= count)
                {
                    // Remove item and return true
                    inventorySlots[i].Count -= count;
                    return true;
                }
            }

            // If no slots have item, return false
            return false;
        }

        // Selects given slot
        public void SelectSlot(int slotIndex)
        {
            // Deselect old slot and set new selected slot
            inventorySlots[selectedSlotIndex].SetSelected(false);
            selectedSlotIndex = slotIndex;
            inventorySlots[selectedSlotIndex].SetSelected(true);
        }

        private void Update()
        {
            GetHotbarKey();
        }

        private void GetHotbarKey()
        {
            // For each hotbar key
            for (int i = 0; i < HotbarKeys.Length; i++)
            {
                // If key pressed, select slot and return
                if (Input.GetKeyDown(HotbarKeys[i]))
                {
                    SelectSlot(i);
                    return;
                }
            }
        }
    }
}
