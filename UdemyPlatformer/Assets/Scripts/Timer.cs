using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _currentTime;

    private bool _timerStarted = false;
    
    public float NewTimer(float waitTime)
    {
        if (_timerStarted)
        {
            _currentTime -= Time.deltaTime;
            return _currentTime;
        }
        else if (!_timerStarted)
        {
            _currentTime = waitTime;
            _timerStarted = true;
            return _currentTime;
        }
        else return 0;
    }
}
