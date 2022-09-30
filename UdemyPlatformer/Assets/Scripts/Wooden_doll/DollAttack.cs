using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollAttack : MonoBehaviour
{
    [SerializeField] private float damage = 30;
    [SerializeField] private float attackDuration = 1f;

    private bool _isDamage = true;

    private float _damageTime;


    private void Start()
    {
        _damageTime = attackDuration;
    }
    private void Update()
    {
        if (!_isDamage)
        {
            _damageTime -= Time.deltaTime;
            if(_damageTime <= 0)
            {
                _isDamage = true;
                _damageTime = attackDuration;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();

        if (playerHP != null && _isDamage)
        {
            playerHP.ReduceHealth(damage);
            _isDamage = false;
        }
    }
}
