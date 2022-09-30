using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isAttack;

    public void FinishAttack()
    {
        _isAttack = false;
    }

    public bool IsAttack { get => _isAttack; }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
            animator.SetTrigger("attack");
        }
    }
}
