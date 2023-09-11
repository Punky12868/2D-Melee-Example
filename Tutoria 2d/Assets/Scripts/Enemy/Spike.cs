using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;
    bool facingRight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }

            collision.GetComponent<PlayerHealth>().HurtPlayer(damage);
            collision.GetComponent<PlayerHealth>().KnockbackPlayer(facingRight);
        }
    }
}
