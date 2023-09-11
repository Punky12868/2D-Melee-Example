using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool targetEnemyHealth;
    PlayerMovement movementScript;

    public Transform fist;
    public LayerMask EnemyLayer;

    public float attackRange = 0.5f;
    public int attackDamage = 25;

    public float attackRate = 2f;
    float attackCooldown = 0;
    bool facingRight;

    private float xPositivePosition, xNegativePosition;
    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        xPositivePosition = fist.position.x;
        xNegativePosition = xPositivePosition * -1;
    }
    private void Update()
    {
        if (Time.time >= attackCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                attackCooldown = Time.time + 1 / attackRate;
            }
        }
        
        FlipFist();
    }
    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fist.position, attackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit Enemy");
            if (enemy.transform.position.x < transform.position.x)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }

            if (!targetEnemyHealth)
            {
                enemy.GetComponent<Health>().Damaged(attackDamage);
                enemy.GetComponent<Health>().Knockback(facingRight);
            }
            else
            {
                enemy.GetComponent<EnemyHealth>().GetHurt(attackDamage);
                enemy.GetComponent<EnemyHealth>().KnockbackEnemy(facingRight);
            }
        }
    }
    private void FlipFist()
    {
        if (movementScript.h > 0)
        {
            fist.localPosition = new Vector2(xPositivePosition, fist.localPosition.y);
        }
        else if (movementScript.h < 0)
        {
            fist.localPosition = new Vector2(xNegativePosition, fist.localPosition.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(fist.position, attackRange);
    }
}
