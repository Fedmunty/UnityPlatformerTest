using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollVision : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float maxDistance;

    private DollController _dollController;

    private Vector2 _origin;
    private Vector2 _direction;

    private GameObject _currentHitObject;

    private const float _circleRadius = 0.5f;

    private float _currentHitDistance;


    private void Awake()
    {
        _dollController = GetComponent<DollController>();
    }
    private void Update()
    {
        _origin = transform.position;

        if (_dollController.IsFacingRight)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.CircleCast(
            _origin, _circleRadius, _direction, maxDistance, layerMask);

        if (hit)
        {
            _currentHitObject = hit.transform.gameObject;
            _currentHitDistance = hit.distance;
            if (_currentHitObject.CompareTag("Player"))
            {
                _dollController.StartChasingPlayer();
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
