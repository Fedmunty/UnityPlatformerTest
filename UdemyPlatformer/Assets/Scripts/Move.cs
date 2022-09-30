using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    private Rigidbody2D _rigidbody;

    private float _velocity;

    public Move(Rigidbody2D rigidbody, float velocity)
    {
        _velocity = velocity;
        _rigidbody = rigidbody;
    }

    private void GetGoing()
    {

    }
}
