using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Animator animator;

    [SerializeField] private float speedX;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashCost;
    
    private float _horizontal = 0f;
    private float _dashTimer;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isLeft = false;
    private bool _isFinish = false;
    private bool _isDash = false;

    private Rigidbody2D _rb;
    private Finish _finish;

    const float speedMultiplier = 200.0f;
    const float dashMultiplier = 4f;
    const float dashDelay = 0.2f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _dashTimer = dashDelay;
    }

    private void Update()
    {
        Controls();
        if (_isDash)
            DashTimer();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(
                _horizontal * speedX * speedMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _isFinish = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _isFinish = false;
        }
    }
    private void Controls()
    {
        _horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("SpeedX", Mathf.Abs(_horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _isJump = true;
        }

        if (Input.GetKey(KeyCode.D) && _isLeft || Input.GetKey(KeyCode.A) && !_isLeft)
        {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.F) && _isFinish)
        {
            _finish.FinishLevel();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDash) 
        {
            Dash();
            Debug.Log("Input");
        }
    }
    private void Jump()
    {
        _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        _isGround = false;
        _isJump = false;
    }
    private void Dash()
    {
        PlayerEnergy playerEnergy = GetComponent<PlayerEnergy>();

        if (playerEnergy.EnergyCheck(dashCost))
        {
            _isDash = true;
            speedX *= dashMultiplier;
            playerEnergy.ReduceEnergy(dashCost);
            Debug.Log("Dash");
        } 
    }
    private void Flip()
    {
        Vector3 playerScale = playerModelTransform.localScale;

        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;

        _isLeft = !_isLeft;
    }
    private void DashTimer()
    {
        if (_dashTimer > 0)
            _dashTimer -= Time.deltaTime;
        else if (_dashTimer <= 0)
        {
            speedX /= dashMultiplier;
            _isDash = false;
            _dashTimer = dashDelay;
        }
    }
}
