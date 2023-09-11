using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 25;
    bool facingRight;
    [HideInInspector] public bool disable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!disable)
        {
            if (collision.GetComponent<Health>())
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    facingRight = true;
                }
                else
                {
                    facingRight = false;
                }

                collision.GetComponent<Health>().Damaged(damage);
                collision.GetComponent<Health>().Knockback(facingRight);
            }
        }
    }
}
