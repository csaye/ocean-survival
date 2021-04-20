using UnityEngine;

namespace OceanSurvival
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D rb;

        private const float Speed = 3;

        private Vector2 direction;

        private void Update()
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
    }
}
