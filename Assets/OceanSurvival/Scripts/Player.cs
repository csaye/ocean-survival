using UnityEngine;

namespace OceanSurvival
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerCamera playerCamera;

        private const float Speed = 3;

        private Vector2 direction;

        private void Update()
        {
            Move();
            Animate();
            Interact();
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
            // Click
            if (Input.GetMouseButton(0))
            {
                // Get tile at mouse position
                Vector2 mousePosition = playerCamera.MouseWorldPoint;
                int tileX = (int)Mathf.Floor(mousePosition.x);
                int tileY = (int)Mathf.Floor(mousePosition.y);
                Vector2Int tilePosition = new Vector2Int(tileX, tileY);

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
