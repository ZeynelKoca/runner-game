using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float GRAVITY_VALUE = 20;

    public int LaneDistance;
    public float JumpForce;
    public float MovementSpeed;

    public SwipeManager SwipeManager;
    public CharacterController CharacterController;
    public Animator Animator;

    private Vector3 _playerVelocity;
    private bool _jump;
    private bool _moveLeft;
    private bool _moveRight;
    private bool _canMove = true;
    private int _currentLane = 0;

    // Start is called before the first frame update
    void Start()
    {
        SwipeManager.OnSwipeDetected += OnSwipeDetectedHandler;
    }

    // Update is called once per frame
    void Update()
    {
        var move = Vector3.forward;
        CharacterController.Move(move * Time.deltaTime * MovementSpeed);

        if (_moveLeft)
        {
            StartCoroutine(MoveLane(-1));
        }
        else if (_moveRight)
        {
            StartCoroutine(MoveLane(+1));
        }

        // Changes the height position of the player
        if (_jump && CharacterController.isGrounded)
        {
            _playerVelocity.y = JumpForce;
            Animator.SetBool("IsJumping", true);
            _jump = false;
            var coroutine = StopAnimation("IsJumping");
            StartCoroutine(coroutine);
        }

        _playerVelocity.y -= GRAVITY_VALUE * Time.deltaTime;
        CharacterController.Move(_playerVelocity * Time.deltaTime);
    }

    IEnumerator MoveLane(int laneOffset)
    {
        CharacterController.Move(new Vector3(laneOffset, 0, 0) * Time.deltaTime * 4.4f);
        yield return new WaitForSeconds(0.5f);

        _moveLeft = false;
        _moveRight = false;
        _canMove = true;
    }

    private void OnSwipeDetectedHandler(Swipe direction, Vector2 swipeVelocity)
    {
        switch (direction)
        {
            case Swipe.Up:
                _jump = true;
                break;
            case Swipe.Left:
                if (_canMove && _currentLane - 1 >= -1)
                {
                    _canMove = false;
                    _moveLeft = true;
                    _currentLane -= 1;
                    Debug.Log($"Player moved to lane {_currentLane}");
                }
                break;
            case Swipe.Right:
                if (_canMove && _currentLane + 1 <= 1)
                {
                    _canMove = false;
                    _moveRight = true;
                    _currentLane += 1;
                    Debug.Log($"Player moved to lane {_currentLane}");
                }
                break;
        }
    }

    IEnumerator StopAnimation(string animationName)
    {
        yield return new WaitForSeconds(1);
        Animator.SetBool(animationName, false);
    }
}
