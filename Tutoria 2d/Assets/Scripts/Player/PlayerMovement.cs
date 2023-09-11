using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    KnockbackScript knockbackScript;
    Grounded grounded;
    [HideInInspector] public float h;
    public float speed, runSpeed, jumpForce, gravityModifier;
    private float walkSpeed;

    Rigidbody2D rb;
    private void Start()
    {
        knockbackScript = GetComponent<KnockbackScript>();
        grounded = GetComponent<Grounded>();
        rb = GetComponent<Rigidbody2D>();
        walkSpeed = speed;
    }

    private void Update()
    {
        //h = Input.GetAxis("Horizontal");
        h = Input.GetAxisRaw("Horizontal");
        if (!knockbackScript.knockback)
        {
            Run();
            Jump();
            Flip();
        }

        ChangeGravity();
    }
    private void FixedUpdate()
    {
        if (!knockbackScript.knockback)
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded.IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void ChangeGravity()
    {
        if (rb.velocity.y < 0 && !grounded.IsGrounded())
        {
            rb.velocity += Vector2.down * gravityModifier * Time.deltaTime;
        }
    }
    private void Flip()
    {
        if (h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (h < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
