using UnityEngine;
using UnityEngine.Tilemaps;

namespace OceanSurvival
{
    public class Map : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tile waterTile, raftTile;

        public static Map Instance;

        public const int MinX = -16;
        public const int MinY = -16;
        public const int MaxX = 16;
        public const int MaxY = 16;

        private const int RaftMinX = -1;
        private const int RaftMinY = -1;
        private const int RaftMaxX = 1;
        private const int RaftMaxY = 1;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Fill map with water
            for (int x = MinX; x < MaxX; x++)
            {
                for (int y = MinY; y < MaxY; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    groundTilemap.SetTile(position, waterTile);
                }
            }

            // Fill raft tiles
            for (int x = RaftMinX; x < RaftMaxX; x++)
            {
                for (int y = RaftMinY; y < RaftMaxY; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    groundTilemap.SetTile(position, raftTile);
                }
            }
        }

        // Sets given position to raft tile
        public bool SetRaft(Vector2Int position)
        {
            // If position already raft, return false
            if (groundTilemap.GetTile((Vector3Int)position) == raftTile) return false;

            // If position not raft, set raft and return true
            groundTilemap.SetTile((Vector3Int)position, raftTile);
            return true;
        }
    }
}
