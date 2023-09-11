using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    bool dead;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void HurtPlayer(int damage)
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
    public void KnockbackPlayer(bool facingRight)
    {
        GetComponent<KnockbackScript>().knockFromRight = facingRight;
        GetComponent<KnockbackScript>().knockback = true;
    }
    private void Death()
    {
        dead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
    }
}
