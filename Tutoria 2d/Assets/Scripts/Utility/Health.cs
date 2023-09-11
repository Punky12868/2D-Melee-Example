using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool player;
    public bool dealsDamage;

    public int maxHealth = 100;
    private int currentHealth = 0;
    bool dead;
    private void Start()
    {
        currentHealth = maxHealth;

        if (player)
        {
            dealsDamage = false;
        }
    }
    public void Damaged(int damage)
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
    public void Knockback(bool facingRight)
    {
        if (!dead)
        {
            GetComponent<KnockbackScript>().knockFromRight = facingRight;
            GetComponent<KnockbackScript>().knockback = true;
        }
    }
    private void Death()
    {
        dead = true;

        GetComponent<Rigidbody2D>().mass = 1;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<KnockbackScript>().knockbackForce = new Vector2(0, 5);
        GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);

        if (player)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
        }
        else if (dealsDamage)
        {
            GetComponent<Damage>().disable = true;
        }
        
    }
}
