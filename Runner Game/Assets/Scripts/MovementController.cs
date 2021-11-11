using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private const float GRAVITY_VALUE = 20;

    public float LaneChangeDuration;
    public float LaneDashSpeed;
    public float JumpForce;
    public float MovementSpeed;
    public float MovementAcceleration;

    public SwipeManager SwipeManager;
    public CharacterController CharacterController;
    public Animator Animator;

    private Vector3 _playerVelocity;
    private bool _jump;
    private bool _canMove;
    private int _currentLane;

    // Start is called before the first frame update
    void Start()
    {
        _canMove = true;
        _currentLane = 1;

        SwipeManager.OnSwipeDetected += OnSwipeDetectedHandler;
    }


    // Update is called once per frame
    void Update()
    {
        var move = Vector3.forward;
        CharacterController.Move(move * Time.deltaTime * MovementSpeed);

        // Changes the height position of the player
        if (_jump && CharacterController.isGrounded)
        {
            _playerVelocity.y = JumpForce;
            _jump = false;
            StartCoroutine(RunAnimationFor(0.9f, "IsJumping"));
        }

        _playerVelocity.y -= GRAVITY_VALUE * Time.deltaTime;
        CharacterController.Move(_playerVelocity * Time.deltaTime);

        MovementSpeed += MovementAcceleration * Time.deltaTime;
        LaneDashSpeed += MovementAcceleration * Time.deltaTime;
    }

    IEnumerator MoveLane(int laneOffset)
    {
        _canMove = false;
        var startPosition = transform.position;

        _ = laneOffset == -1
            ? StartCoroutine(RunAnimationFor(LaneChangeDuration / 10, "IsRunningLeft"))
            : StartCoroutine(RunAnimationFor(LaneChangeDuration / 10, "IsRunningRight"));

        while (LaneChangeDuration > Vector3.Distance(startPosition, transform.position))
        {
            CharacterController.Move(transform.TransformDirection(new Vector3(laneOffset, 0, 0)) * LaneDashSpeed * Time.deltaTime);
            yield return null;
        }

        _currentLane += laneOffset;
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
                if (_canMove && _currentLane - 1 >= 0)
                {
                    StartCoroutine(MoveLane(-1));
                }
                break;
            case Swipe.Right:
                if (_canMove && _currentLane + 1 <= 2)
                {
                    StartCoroutine(MoveLane(+1));
                }
                break;
        }
    }

    IEnumerator RunAnimationFor(float secondsWaitTime, string animationName)
    {
        Animator.SetBool(animationName, true);
        yield return new WaitForSeconds(secondsWaitTime);
        Animator.SetBool(animationName, false);
    }
}
