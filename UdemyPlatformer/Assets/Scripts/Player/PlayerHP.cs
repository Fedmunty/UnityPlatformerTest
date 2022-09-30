using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Slider healthsSlider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOverCanvas;

    [SerializeField] private float totalHealth = 100f;


    private float _health;

    private void Start()
    {
        gameOverCanvas.SetActive(false);

        _health = totalHealth;
        InitHealth();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();
        animator.SetTrigger("takeDamage");

        if (_health <= 0)
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
        gameOverCanvas.SetActive(true);
    }
}
