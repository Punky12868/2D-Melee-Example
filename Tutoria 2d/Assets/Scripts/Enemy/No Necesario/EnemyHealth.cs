using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    bool dead;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetHurt(int damage)
    {
        if (!dead)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }
    public void KnockbackEnemy(bool facingRight)
    {
        GetComponent<KnockbackScript>().knockFromRight = facingRight;
        GetComponent<KnockbackScript>().knockback = true;
    }
    void Death()
    {
        dead = true;

        GetComponent<Rigidbody2D>().mass = 1;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<KnockbackScript>().knockbackForce = new Vector2(0, 5);
        GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
    }
}
