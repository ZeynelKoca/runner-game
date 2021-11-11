using UnityEngine;

public class ChunkRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject.transform.root.gameObject);
    }
}
