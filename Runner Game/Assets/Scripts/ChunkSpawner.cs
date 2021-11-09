using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject ChunkPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Add logic for randomly spawning items on the chunk
        Instantiate(ChunkPrefab, transform.position + new Vector3(-4.88f, -6.08f, 56.65f), Quaternion.identity);
    }
}
