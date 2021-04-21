using UnityEngine;
using UnityEngine.EventSystems;

namespace OceanSurvival
{
    public static class Operation
    {
        private const float Epsilon = 0.05f;
        private static readonly Vector2 TileSize = Vector2.one - new Vector2(Epsilon, Epsilon);
        private static readonly Vector2 Vector2Half = new Vector2(0.5f, 0.5f);

        // Returns whether mouse is currently over any UI
        public static bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();

        // Returns all colliders within tile at given position
        public static Collider2D[] CollidersInTile(Vector2Int position)
        {
            return Physics2D.OverlapBoxAll(position + Vector2Half, TileSize, 0);
        }

        // Linearly interpolates between two colors
        public static Color LerpColor(Color a, Color b, float t) => (1 - t) * a + t * b;
    }
}
