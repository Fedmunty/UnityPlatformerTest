using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Slider healthsSlider;

    [SerializeField] private float totalHealth = 100f;

    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }
    public void ReduceHealth(float damage)
    {
        animator.SetTrigger("takeDamage");

        _health -= damage;
        InitHealth();

        if(_health <= 0)
        {
            Die();
        }
    }

    private void InitHealth()
    {
        healthsSlider.value = _health / totalHealth;
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
