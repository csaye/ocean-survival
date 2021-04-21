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

        public Sprite GetItemSprite(int itemID) => itemSprites[itemID];

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SelectSlot(selectedSlotIndex);
        }

        // Adds given item to inventory
        public void AddItem(Item item) => AddItem((int)item, 1);
        public void AddItem(int itemID) => AddItem(itemID, 1);
        public void AddItem(int itemID, int count)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // Get item at inventory position
                InventorySlot slot = inventorySlots[i];

                // If item at position empty, set item
                if (slot.IsEmpty)
                {
                    inventorySlots[i].SetSlot(itemID, count);
                    return;
                }
                // If stackable to position, stack
                else if (slot.ItemID == itemID)
                {
                    inventorySlots[i].Count += count;
                    return;
                }
            }
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
