using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject[] JumpableObstaclePrefabs;
    public GameObject[] SlideableObstaclePrefabs;
    public GameObject EmptyChunkPrefab;
    public GameObject ChunkSpawnTriggerPrefab;
    public GameObject CoinPrefab;

    public float CoinSpacing;
    public float RoadChunkLength;
    public float RoadChunkWidth;

    private RandomWeightedItemGenerator _randomWeightedItemGenerator;

    // Start is called before the first frame update
    void Start()
    {
        _randomWeightedItemGenerator = new RandomWeightedItemGenerator(
            new WeightedItem(Item.Nothing, 4),
            new WeightedItem(Item.Coin, 3),
            new WeightedItem(Item.JumpableObstacle, 2),
            new WeightedItem(Item.SlideableObstacle, 1)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenerateChunks();
        }
    }

    private void GenerateChunks()
    {
        // Create the first chunk with a new chunk spawn triggerbox.
        GameObject headChunk = Instantiate(EmptyChunkPrefab, transform.position + new Vector3(-4.88f, -6.08f, 64.7f), Quaternion.identity);
        Instantiate(ChunkSpawnTriggerPrefab, headChunk.transform);

        Transform headTransform = headChunk.transform;
        // Subsequently generate 4 chunks to fill screen view
        for (int i = 0; i < 4; i++)
        {
            GameObject runnableChunk = Instantiate(EmptyChunkPrefab, headTransform.position + new Vector3(0, 0, 11.75f), Quaternion.identity);
            headTransform = runnableChunk.transform;

            GenerateItems(runnableChunk);
        }
    }

    private void GenerateItems(GameObject runnableChunk)
    {
        var items = CreateItemsWrapper(runnableChunk);

        // Each runnable chunk can store 2 rows of obstacles on 3 different lanes.
        for (int road = 0; road < 2; road++)
        {
            for (int lane = 0; lane < 3; lane++)
            {
                Item randomItem = _randomWeightedItemGenerator.GetRandomItem();
                switch (randomItem)
                {
                    case Item.JumpableObstacle:
                        SpawnRandomJumpableObstacle(road, lane, items);
                        break;
                    case Item.SlideableObstacle:
                        SpawnRandomSlideableObstacle(road, lane, items);
                        break;
                    case Item.Coin:
                        SpawnCoin(road, lane, items);
                        break;
                }
            }
        }
    }

    private GameObject CreateItemsWrapper(GameObject runnableChunk)
    {
        DestroyExistingItemsWrapper(runnableChunk);

        GameObject roads = runnableChunk.transform.Find("Road tiles").gameObject;
        GameObject items = new GameObject("Items");
        items.transform.SetParent(runnableChunk.transform);
        items.transform.localPosition = roads.transform.localPosition;

        return items;
    }

    private void DestroyExistingItemsWrapper(GameObject runnableChunk)
    {
        var itemsWrapper = runnableChunk.transform.Find("Items");
        if (itemsWrapper != null)
        {
            Destroy(itemsWrapper.gameObject);
        }
    }

    private void SpawnRandomJumpableObstacle(int roadIndex, int laneIndex, GameObject itemsWrapper)
    {
        SpawnRandomObstacle(JumpableObstaclePrefabs, roadIndex, laneIndex, itemsWrapper);
    }

    private void SpawnRandomSlideableObstacle(int roadIndex, int laneIndex, GameObject itemsWrapper)
    {
        SpawnRandomObstacle(SlideableObstaclePrefabs, roadIndex, laneIndex, itemsWrapper);
    }

    private void SpawnRandomObstacle(GameObject[] obstaclePrefabs, int roadIndex, int laneIndex, GameObject itemsWrapper)
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        float xOffset = laneIndex == 0 ? 1f : 2.3f * laneIndex + 1f;
        var obstacle = Instantiate(obstaclePrefabs[randomIndex], itemsWrapper.transform, false);
        obstacle.transform.localPosition += new Vector3(xOffset, 0, roadIndex * 6);
    }

    private void SpawnCoin(int roadIndex, int laneIndex, GameObject itemsWrapper)
    {
        float xOffset = laneIndex == 0 ? 1f : 2.3f * laneIndex + 1f;
        var coin = Instantiate(CoinPrefab, itemsWrapper.transform, false);
        coin.transform.localPosition += new Vector3(xOffset, 0.4f, roadIndex * 6);
    }
}
