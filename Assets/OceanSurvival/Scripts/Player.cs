using OceanSurvival.UI;
using UnityEngine;

namespace OceanSurvival
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerCamera playerCamera;
        
        [Header("Highlight References")]
        [SerializeField] private Transform highlightTransform;
        [SerializeField] private SpriteRenderer highlightSpriteRenderer;
        [SerializeField] private Sprite transparent, highlight;

        private const float Speed = 3;
        private const float MaxReach = 6;

        private readonly Item[] FishingItems = new Item[]
        {
            Item.Wood,
            Item.Stone,
            Item.Seaweed,
            Item.Fish
        };
        private Item FishingItem() => FishingItems[Random.Range(0, FishingItems.Length)];

        private Vector2 direction;

        private void Update()
        {
            // If game over, hide highlight
            if (UIManager.Instance.GameOver) highlightSpriteRenderer.sprite = transparent;
            // If game not over
            else
            {
                if (!UIManager.Instance.MenuOpen) Move(); // If menu not open, move
                Animate(); // Animate player based on movement
                Interact(); // Check for interactions
            }
        }

        private void Move()
        {
            // Get movement direction
            direction = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) direction.y += 1;
            if (Input.GetKey(KeyCode.S)) direction.y -= 1;
            if (Input.GetKey(KeyCode.D)) direction.x += 1;
            if (Input.GetKey(KeyCode.A)) direction.x -= 1;
            direction.Normalize();

            // Update rigidbody velocity
            rb.velocity = direction * Speed;
        }

        private void Animate()
        {
            // Set animator values
            if (direction != Vector2.zero)
            {
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
            }
            animator.SetFloat("Speed", direction.magnitude);
        }

        private void Interact()
        {
            // Get tile at mouse position
            Vector2 mousePosition = playerCamera.MouseWorldPoint;
            int tileX = (int)Mathf.Floor(mousePosition.x);
            int tileY = (int)Mathf.Floor(mousePosition.y);
            Vector2Int tilePosition = new Vector2Int(tileX, tileY);

            // If out of reach or mouse over UI
            bool outOfReach = Vector2.Distance(transform.position, tilePosition) > MaxReach;
            if (outOfReach || Operation.IsMouseOverUI())
            {
                // Disable highlight and return
                highlightSpriteRenderer.sprite = transparent;
                return;
            }
            
            // Enable highlight and move to position
            highlightSpriteRenderer.sprite = highlight;
            highlightTransform.position = (Vector3Int)tilePosition;

            // If click
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                // For each collider in tile position
                foreach (Collider2D col in Operation.CollidersInTile(tilePosition))
                {
                    // Get interactable component
                    IInteractable interactableComponent = col.GetComponent<IInteractable>();

                    // If interactable component not null
                    if (interactableComponent != null)
                    {
                        // Interact and return
                        interactableComponent.Interact();
                        return;
                    }
                }

                // Use selected item
                Item selectedItem = Inventory.Instance.SelectedSlot.Item;
                
                // Set tile to raft
                if (selectedItem == Item.Raft)
                {
                    // If raft placed, decrement inventory slot
                    if (Map.Instance.SetRaft(tilePosition)) Inventory.Instance.SelectedSlot.Count--;
                }
                // Set tile to stone raft
                else if (selectedItem == Item.StoneRaft)
                {
                    // If stone raft placed, decrement inventory slot
                    if (Map.Instance.SetStoneRaft(tilePosition)) Inventory.Instance.SelectedSlot.Count--;
                }
                // Eat seaweed
                else if (selectedItem == Item.Seaweed)
                {
                    // If energy not maxed
                    if (EnergyBar.Instance.Energy < EnergyBar.MaxEnergy)
                    {
                        // Increment energy and decrement slot
                        EnergyBar.Instance.Energy += 5;
                        Inventory.Instance.SelectedSlot.Count--;
                    }
                }
                // Eat fish
                else if (selectedItem == Item.Fish)
                {
                    // If energy not maxed
                    if (EnergyBar.Instance.Energy < EnergyBar.MaxEnergy)
                    {
                        // Increment energy and decrement slot
                        EnergyBar.Instance.Energy += 15;
                        Inventory.Instance.SelectedSlot.Count--;
                    }
                }
                // Use fishing rod
                else if (selectedItem == Item.FishingRod)
                {
                    // If water tile at position
                    if (Map.Instance.IsWaterTile(tilePosition))
                    {
                        // Decrement energy
                        EnergyBar.Instance.Energy -= 10;

                        // Add random item to inventory
                        Inventory.Instance.AddItem(FishingItem(), 1);
                    }
                }
            }
        }
    }
}
