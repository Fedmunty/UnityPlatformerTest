using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    [SerializeField] private float damage;

    private UFOVision _UFOVision;
    private Rigidbody2D _rb;

    private Vector2 _chasingDistanse;
    private Vector2 _Nextpiont;
    private Vector2 _velocity;
    private Vector2 _attackPoint;

    private float _delay = 2f;

    private bool _attackPlayer = false;


    public bool IsAttackPlayer { get => _attackPlayer; }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _UFOVision = GetComponent<UFOVision>();
    }
    private void Update()
    {
        if (_attackPlayer)
        {
            _delay -= Time.deltaTime;
            if (_delay <= 0)
            {
                _Nextpiont = _chasingDistanse * Time.fixedDeltaTime;
                _rb.MovePosition((Vector2)transform.position - _Nextpiont);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();

            if (playerHP != null)
            {
                playerHP.ReduceHealth(damage);
            }
            gameObject.SetActive(false);
        }
    }

    public void AttackPlayer()
    {
        _attackPoint = _UFOVision.CurrentHitObject.transform.position;
        _chasingDistanse = (Vector2)transform.position - _attackPoint;

        _attackPlayer = true;
        Debug.Log("Attacking");
    }
    
}
