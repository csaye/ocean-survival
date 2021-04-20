using UnityEngine;
using UnityEngine.Tilemaps;

namespace OceanSurvival
{
    public class Map : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tile waterTile;

        public const int MinX = -16;
        public const int MinY = -16;
        public const int MaxX = 16;
        public const int MaxY = 16;

        private void Start()
        {
            for (int x = MinX; x < MaxX; x++)
            {
                for (int y = MinY; y < MaxY; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    groundTilemap.SetTile(position, waterTile);
                }
            }
        }
    }
}
