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

        private Vector2 direction;

        private void Update()
        {
            if (!UIManager.Instance.MenuOpen) Move(); // If menu not open, move
            Animate(); // Animate player based on movement
            Interact(); // Check for interactions
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

            // If mouse over UI or tile out of reach
            if (Operation.IsMouseOverUI() || Vector2.Distance(transform.position, tilePosition) > MaxReach)
            {
                // Disable highlight and return
                highlightSpriteRenderer.sprite = transparent;
                return;
            }
            
            // Enable highlight and move to position
            highlightSpriteRenderer.sprite = highlight;
            highlightTransform.position = (Vector3Int)tilePosition;

            // If click
            if (Input.GetMouseButton(0))
            {
                // For each collider in tile position
                foreach (Collider2D col in Operation.CollidersInTile(tilePosition))
                {
                    // Get interactable component
                    IInteractable interactableComponent = col.GetComponent<IInteractable>();

                    // If interactable component not null
                    if (interactableComponent != null)
                    {
                        // Interact and break
                        interactableComponent.Interact();
                        break;
                    }
                }
            }
        }
    }
}
