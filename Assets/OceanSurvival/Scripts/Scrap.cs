using OceanSurvival.UI;
using UnityEngine;

namespace OceanSurvival
{
    public class Scrap : MonoBehaviour, IInteractable
    {
        [Header("Attributes")]
        [SerializeField] private ItemCount pickupItem;
        
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;

        private const float Speed = 1;

        public void Initialize(Vector2Int direction)
        {
            // Set rigidbody velocity
            rb.velocity = (Vector2)direction * Speed;
        }

        public void Interact()
        {
            // Add item to inventory
            Inventory.Instance.AddItem(pickupItem);

            // Destroy self
            Destroy(gameObject);
        }

        private void Update()
        {
            // Destroy self if out of bounds
            Vector2 position = transform.position;
            if (position.x < Map.MinX || position.x > Map.MaxX || position.y < Map.MinY || position.y > Map.MaxY)
            {
                Destroy(gameObject);
            }
        }
    }
}
