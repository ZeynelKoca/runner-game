using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float JumpForce;
    public SwipeManager SwipeManager;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        SwipeManager.OnSwipeDetected += OnSwipeDetectedHandler;

        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSwipeDetectedHandler(Swipe direction, Vector2 swipeVelocity)
    {
        if(direction == Swipe.Up)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * JumpForce);
    }
}
