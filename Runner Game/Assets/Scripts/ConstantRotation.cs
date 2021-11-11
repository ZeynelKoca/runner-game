using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float XRotationSpeed;
    public float YRotationSpeed;
    public float ZRotationSpeed;

    // Update is called once per frame
    void Update()
    {
        var x = XRotationSpeed * Time.deltaTime;
        var y = YRotationSpeed * Time.deltaTime;
        var z = ZRotationSpeed * Time.deltaTime;

        transform.Rotate(x, y, z);
    }
}
