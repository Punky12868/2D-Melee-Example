using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    Grounded grounded;
    public bool waitForGrounded;

    bool knockbackEffect = true;
    public bool knockback;
    public bool knockFromRight;
    public Vector2 knockbackForce;
    public float knockbackTime = 0.8f;
    float knockbackStoredTime;
    private void Start()
    {
        knockbackStoredTime = knockbackTime;
        grounded = GetComponent<Grounded>();
    }
    private void Update()
    {
        if (knockback)
        {
            CancelKnockback();
        }
    }
    private void FixedUpdate()
    {
        if (waitForGrounded)
        {
            if (knockback)
            {
                if (knockbackEffect)
                {
                    if (knockFromRight)
                    {
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockbackForce.x, knockbackForce.y), ForceMode2D.Impulse);
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackForce.x, knockbackForce.y), ForceMode2D.Impulse);
                    }
                    knockbackEffect = false;
                }
            }
        }
        else
        {
            if (knockback)
            {
                if (knockbackEffect)
                {
                    if (knockFromRight)
                    {
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockbackForce.x, knockbackForce.y), ForceMode2D.Impulse);
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackForce.x, knockbackForce.y), ForceMode2D.Impulse);
                    }
                    knockbackEffect = false;
                }
            }
        }
        
    }
    private void CancelKnockback()
    {
        if (knockbackTime <= 0)
        {
            if (waitForGrounded)
            {
                if (grounded.IsGrounded())
                {
                    knockback = false;
                    knockbackEffect = true;
                    knockbackTime = knockbackStoredTime;
                }
            }
            else
            {
                knockback = false;
                knockbackEffect = true;
                knockbackTime = knockbackStoredTime;
            }
        }
        else
        {
            knockbackTime -= Time.deltaTime;
        }
    }
}
