using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOVision : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private float maxDistance;

    private GameObject _currentHitObject;
    private Transform _playerTransform;
    private UFOController _UFOController;


    private Vector2 _direction;
    private Vector2 _origin;

    private float _currentHitDistance;

    private const float _circleRadius = 0.5f;

    public GameObject CurrentHitObject { get => _currentHitObject; }
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _UFOController = GetComponent<UFOController>();
    }

    private void Update()
    {
        _direction = (Vector2)_playerTransform.position - (Vector2)transform.position;
        _origin = (Vector2)transform.position;

        RaycastHit2D hit = Physics2D.CircleCast(_origin, _circleRadius, _direction, maxDistance, layerMask);

        if (hit && !_UFOController.IsAttackPlayer)
        {
            _currentHitObject = hit.transform.gameObject;
            _currentHitDistance = hit.distance;

            if (_currentHitObject.CompareTag("Player"))
            {
                _UFOController.AttackPlayer();
            }
        }
        else
        {
            _currentHitObject = null;
            _currentHitDistance = maxDistance;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_origin, _origin + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, _circleRadius);
    }


}
