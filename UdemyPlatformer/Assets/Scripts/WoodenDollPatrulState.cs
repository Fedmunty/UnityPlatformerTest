using System;
using UnityEngine;

public class WoodenDollPatrulState : IState
{
    private Transform _modelTransform;

    private float _leftBound;
    private float _rightBound;

    private bool _isFacingRight;

    public void Enter()
    {
        Debug.Log("Enter");
    }
    public void Exit()
    {
        Debug.Log("Exit");

    }
    public void Update()
    {
        Debug.Log("Updating");
        
    }
    public bool OutOfBound(Transform transform, float rightBound, float leftBound, bool isFacingRight)
    {
        {
            Debug.Log(rightBound);
            Debug.Log(leftBound);
            Debug.Log(isFacingRight);

            bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= rightBound;
            bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= leftBound;

            return isOutOfLeftBoundary || isOutOfRightBoundary;
        }
    }
    
}
