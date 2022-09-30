using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20;
    private AttackControler _attackControler;

    private void Start()
    {
        _attackControler = transform.root.GetComponent<AttackControler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DollHealth dollHealth = collision.GetComponent<DollHealth>();

        if(dollHealth != null && _attackControler.IsAttack)
        {
            dollHealth.ReduceHealth(damage);
            Debug.Log("Hit");
        }
    }
}
