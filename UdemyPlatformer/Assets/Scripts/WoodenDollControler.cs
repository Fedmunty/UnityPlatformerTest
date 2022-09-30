using System;
using UnityEngine;
using System.Collections.Generic;

public class WoodenDollControler : MonoBehaviour
{
    [SerializeField] private float timeToWait;
    [SerializeField] private float patrulVelocity;

    private WoodenDollPatrulState _patrulState;

    private Dictionary<Type, IState> behaviorsMap;

    private Rigidbody2D _rigidbody;

    private StateMachine _stateMachine;

    private Vector2 _nextPoint;
    private Vector2 _leftBound;
    private Vector2 _rightBound;

    private float _currentVelocity;

    private bool _isFacingRight;

    public bool IsFacingRight { get => _isFacingRight; }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _stateMachine = new StateMachine();
        _patrulState = new WoodenDollPatrulState();

        InitBehaviors();  
        DefaultState(GetState<WoodenDollPatrulState>());

        BoundaryPoints();
        DirectionX(transform);

        _currentVelocity = patrulVelocity;
        
    }
    private void Update()
    {
        _stateMachine.CurrentState.Update();

        if (_patrulState.OutOfBound(transform, _leftBound.x, _rightBound.x, _isFacingRight)) {
            Flip(transform);
        };

    }
    private void FixedUpdate()
    {
        Move(_currentVelocity);
    }

    private void InitBehaviors()
    {
        behaviorsMap = new Dictionary<Type, IState>();

        behaviorsMap[typeof(WoodenDollPatrulState)] = new WoodenDollPatrulState();
        behaviorsMap[typeof(WoodenDollChasingState)] = new WoodenDollChasingState();

    }
    private IState GetState<T>() where T : IState
    {
        var type = typeof(T);
        return behaviorsMap[type];
    }
    private void DefaultState(IState state)
    {
        _stateMachine.Initialize(state);
    }
    

    private void Move(float velocity)
    {
        _nextPoint = Vector2.right * velocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition((Vector2)base.transform.position + _nextPoint);
    }
    private void BoundaryPoints()
    {
        _rightBound = (Vector2)transform.position + Vector2.right * 5;
        _leftBound = (Vector2)transform.position + Vector2.left * 5;
    }
    private bool DirectionX(Transform transform)
    {
        Vector3 dollScale = transform.localScale;

        if (dollScale.x > 0)
        {
            _isFacingRight = false;
        }
        else
            _isFacingRight = true;
        return _isFacingRight;
    }
    private void Flip(Transform transform)
    {
        Vector3 dollScale = transform.localScale;

        dollScale.x *= -1f;
        _isFacingRight = !_isFacingRight;

        transform.localScale = dollScale;

        _nextPoint *= -1;
    }
    
}
