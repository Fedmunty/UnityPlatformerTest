using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    [SerializeField] private Transform dollModelTransform;

    private Rigidbody2D _rb;
    private Transform _playerTransform;

    [SerializeField] private float walkDistance;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float timeToWait;
    [SerializeField] private float timeToChase;
    [SerializeField] private float StopDistance;

    private bool _isFacingRight;
    private bool _isChasingPlayer = false;
    private bool _isWait = false;

    private float _waitTime;

    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;
    private Vector2 _nextPoint;

    public bool IsFacingRight { get => _isFacingRight; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        DirectionX();
        BoundaryPoints();
    }
    private void FixedUpdate()
    {
        Moove();
    }
    private void Update()
    {
        if (OutOfBoundary())
        {
            _isWait = true;
        }
        if (_isWait && !_isChasingPlayer)
        {
            Wait();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }

    private void Flip()
    {
        Vector3 dollScale = dollModelTransform.localScale;

        dollScale.x *= -1f;
        _isFacingRight = !_isFacingRight;

        dollModelTransform.localScale = dollScale;
    }
    private void Moove()
    {
        _nextPoint = Vector2.right * walkSpeed * Time.fixedDeltaTime;

        if (_isChasingPlayer)
        {
            if (Mathf.Abs(DistanceToPlayer()) < StopDistance)
            {
                return;
            }
            ChasePlayer();
            Wait();
        }

        if (!_isWait && !_isChasingPlayer)
        {
            Patrul();
        }
    }
    private void Wait()
    {
        _waitTime -= Time.deltaTime;

        if (_waitTime < 0 && !_isChasingPlayer)
        {
            _waitTime = timeToWait;
            _isWait = false;
            if (!_isChasingPlayer &&
                transform.position.x - _leftBoundaryPosition.x <= 0 && !_isFacingRight ||
                transform.position.x - _rightBoundaryPosition.x >= 0 && _isFacingRight
               )
            {
                Flip();
            }
        }
        if (_waitTime < 0 && _isChasingPlayer)
        {
            _isChasingPlayer = false;
        }
    }
    private bool OutOfBoundary()
    {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;

        return isOutOfLeftBoundary || isOutOfRightBoundary;
    }
    private void DirectionX()
    {
        Vector3 dollScale = transform.localScale;

        if (dollScale.x > 0)
        {
            _isFacingRight = false;
        }
        else
            _isFacingRight = true;
    }
    private void BoundaryPoints()
    {
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
    }
    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _waitTime = timeToChase;
    }
    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();
        if (distance < 0)
        {
            _nextPoint.x *= -1;
        }
        if (distance < 0.2f && _isFacingRight)
        {
            Flip();
        }
        else if (distance > 0.2f && !_isFacingRight)
        {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private void Patrul()
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private float DistanceToPlayer()
    {
        float distance = _playerTransform.position.x - transform.position.x;
        return distance;
    }
}
