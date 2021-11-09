using UnityEngine;

public class ChunkRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        Destroy(other.gameObject.transform.root.gameObject);
    }
}
