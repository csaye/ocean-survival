using UnityEngine;

namespace OceanSurvival
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera cam;
        [SerializeField] private Transform playerTransform;

        public Vector2 MouseWorldPoint => cam.ScreenToWorldPoint(Input.mousePosition);
        private Vector2 MinWorldPoint => cam.ScreenToWorldPoint(Vector2.zero);
        private Vector2 MaxWorldPoint => cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        private void Update()
        {
            // Move to player position
            Vector2 playerPosition = playerTransform.position;

            // Calculate screen midpoint dimensions
            Vector2 screenSize = MaxWorldPoint - MinWorldPoint;
            float midWidth = screenSize.x / 2;
            float midHeight = screenSize.y / 2;

            // Get minimum and maximum camera positions
            float minCameraX = Map.MinX + midWidth;
            float maxCameraX = Map.MaxX - midWidth;
            float minCameraY = Map.MinY + midHeight;
            float maxCameraY = Map.MaxY - midHeight;

            // Clamp camera position
            float cameraX = minCameraX > maxCameraX ? 0 : Mathf.Clamp(playerPosition.x, minCameraX, maxCameraX);
            float cameraY = minCameraY > maxCameraY ? 0 : Mathf.Clamp(playerPosition.y, minCameraY, maxCameraY);

            // Move camera to position
            transform.position = new Vector3(cameraX, cameraY, transform.position.z);
        }
    }
}
