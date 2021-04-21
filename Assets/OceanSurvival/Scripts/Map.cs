using UnityEngine;
using UnityEngine.Tilemaps;

namespace OceanSurvival
{
    public class Map : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tile waterTile, raftTile;
        [SerializeField] private Tile[] stoneRaftTiles;

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

        // Returns whether water tile at given position
        public bool IsWaterTile(Vector2Int position)
        {
            return groundTilemap.GetTile((Vector3Int)position) == waterTile;
        }

        // Sets given position to raft tile
        public bool SetRaft(Vector2Int position)
        {
            // If position not water, return false
            if (!IsWaterTile(position)) return false;

            // Set raft and return true
            groundTilemap.SetTile((Vector3Int)position, raftTile);
            return true;
        }

        // Sets given position to stone raft tile
        public bool SetStoneRaft(Vector2Int position)
        {
            // If any positions not water, return false
            for (int y = position.y; y > position.y - 2; y--)
            {
                for (int x = position.x; x < position.x + 2; x++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    if (!IsWaterTile(pos)) return false;
                }
            }

            // Set stone raft and return true
            int i = 0;
            for (int y = position.y; y > position.y - 2; y--)
            {
                for (int x = position.x; x < position.x + 2; x++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    groundTilemap.SetTile((Vector3Int)pos, stoneRaftTiles[i]);
                    i++;
                }
            }
            return true;
        }
    }
}
