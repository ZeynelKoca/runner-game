using UnityEngine;

public enum Item
{
    Obstacle,
    Coin,
    Nothing,
};

public class ChunkSpawner : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;
    public GameObject EmptyChunkPrefab;
    public GameObject ChunkSpawnTriggerPrefab;
    public GameObject CoinPrefab;

    public float CoinSpacing;
    public float RoadChunkLength;
    public float RoadChunkWidth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Generating new runnable chunks...");
            GenerateChunks();
        }
    }

    private void GenerateChunks()
    {
        // Create the first chunk with a new chunk spawn triggerbox.
        GameObject headChunk = Instantiate(EmptyChunkPrefab, transform.position + new Vector3(-4.88f, -6.08f, 64.7f), Quaternion.identity);
        Instantiate(ChunkSpawnTriggerPrefab, headChunk.transform);

        Transform headTransform = headChunk.transform;
        for (int i = 0; i < 4; i++)
        {
            GameObject runnableChunk = Instantiate(EmptyChunkPrefab, headTransform.position + new Vector3(0, 0, 11.75f), Quaternion.identity);
            headTransform = runnableChunk.transform;

            GenerateItems(runnableChunk);
        }
    }

    private void GenerateItems(GameObject runnableChunk)
    {
        var itemsWrapper = runnableChunk.transform.Find("Items");
        if (itemsWrapper != null)
        {
            Destroy(itemsWrapper.gameObject);
        }
        GameObject roads = runnableChunk.transform.Find("Road tiles").gameObject;
        GameObject items = new GameObject("Items");
        items.transform.SetParent(runnableChunk.transform);
        items.transform.localPosition = roads.transform.localPosition;

        for (int road = 0; road < 2; road++)
        {
            for (int lane = 0; lane < 3; lane++)
            {
                Item randomItem = GetRandomItem();
                switch (randomItem)
                {
                    case Item.Obstacle:
                        SpawnRandomObstacle(road, lane, items);
                        break;
                    case Item.Coin:
                        SpawnCoin(road, lane, items);
                        break;
                }
            }
        }
    }

    private void SpawnRandomObstacle(int roadIndex, int laneIndex, GameObject itemsWrapper)
    {
        int randomIndex = Random.Range(0, ObstaclePrefabs.Length);
        float xOffset = laneIndex == 0 ? 1f : 2.3f * laneIndex + 1f;
        var obstacle = Instantiate(ObstaclePrefabs[randomIndex], itemsWrapper.transform, false);
        obstacle.transform.localPosition += new Vector3(xOffset, 0, roadIndex * 6);
    }

    private void SpawnCoin(int roadIndex, int laneIndex, GameObject itemsWrapper)
    {
        float xOffset = laneIndex == 0 ? 1f : 2.3f * laneIndex + 1f;
        var coin = Instantiate(CoinPrefab, itemsWrapper.transform, false);
        coin.transform.localPosition += new Vector3(xOffset, 0.4f, roadIndex * 6);
    }

    private Item GetRandomItem()
    {
        var allItems = System.Enum.GetValues(typeof(Item));
        int randomNumer = Random.Range(0, allItems.Length);

        return (Item)allItems.GetValue(randomNumer);
    }
}
