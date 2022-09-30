using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float damage = 30.0f;
    [SerializeField] private float repulsivForce = 300.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
        if (playerHP != null)
        {
            playerHP.ReduceHealth(damage);
        }
        playerRigidbody.AddForce(transform.up * repulsivForce, ForceMode2D.Force);
    }
}
