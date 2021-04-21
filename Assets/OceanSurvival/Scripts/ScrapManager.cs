using UnityEngine;

namespace OceanSurvival
{
    public class ScrapManager : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private GameObject[] scrapObjects;

        private GameObject Scrap() => scrapObjects[Random.Range(0, scrapObjects.Length)];

        public void SpawnScrap()
        {
            // Get random position and direction
            Vector2Int position = new Vector2Int();
            Vector2Int direction = new Vector2Int();
            switch (Random.Range(0, 4))
            {
                case 0:
                {
                    int randomX = Random.Range(Map.MinX + 1, Map.MaxX - 1);
                    position = new Vector2Int(randomX, Map.MinY);
                    direction = Vector2Int.up;
                    break;
                }
                case 1:
                {
                    int randomX = Random.Range(Map.MinX + 1, Map.MaxX - 1);
                    position = new Vector2Int(randomX, Map.MaxY - 1);
                    direction = Vector2Int.down;
                    break;
                }
                case 2:
                {
                    int randomY = Random.Range(Map.MinY + 1, Map.MaxY - 1);
                    position = new Vector2Int(Map.MinX, randomY);
                    direction = Vector2Int.right;
                    break;
                }
                case 3:
                {
                    int randomY = Random.Range(Map.MinY + 1, Map.MaxY - 1);
                    position = new Vector2Int(Map.MaxX - 1, randomY);
                    direction = Vector2Int.left;
                    break;
                }
            }

            // Instantiate and initialize scrap
            GameObject obj = Instantiate(Scrap(), (Vector3Int)position, Quaternion.identity, transform);
            obj.GetComponent<Scrap>().Initialize(direction);
        }
    }
}
