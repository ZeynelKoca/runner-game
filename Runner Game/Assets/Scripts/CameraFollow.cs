using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject PlayerObject;
    public float ZPosOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerObject.transform.position.x, transform.position.y, PlayerObject.transform.position.z - ZPosOffset);
    }
}
