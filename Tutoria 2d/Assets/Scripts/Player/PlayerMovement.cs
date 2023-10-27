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

    public float dashForce;

    private bool isDahsing;
    private bool stopDash;

    public float isDahsingCooldown;
    private float dahsingCooldown;

    public float buttonCooldownTime;
    private float buttonCooldown;

    private KeyCode dashKey;
    private KeyCode currentDashKey;

    private int buttonCount;

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
            if (!isDahsing)
            {
                Run();
                Jump();
                Flip();
            }
        }

        ChangeGravity();
    }
    private void FixedUpdate()
    {
        if (!knockbackScript.knockback)
        {
            Dash();

            if (!isDahsing)
            {
                rb.velocity = new Vector2(h * speed, rb.velocity.y);
            }
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
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (buttonCooldown > 0 && buttonCount == 1)
            {
                rb.velocity = new Vector2(dashForce, 0);
                isDahsing = true;
                dahsingCooldown = isDahsingCooldown;
            }
            else
            {
                buttonCooldown = buttonCooldownTime;
                buttonCount += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (buttonCooldown > 0 && buttonCount == 1)
            {
                rb.velocity = new Vector2(-dashForce, 0);
                isDahsing = true;
                dahsingCooldown = isDahsingCooldown;
            }
            else
            {
                buttonCooldown = buttonCooldownTime;
                buttonCount += 1;
            }
        }

        if (buttonCooldown > 0)
        {

            buttonCooldown -= 1 * Time.fixedDeltaTime;

        }
        else
        {
            buttonCount = 0;
        }

        if (dahsingCooldown > 0)
        {

            dahsingCooldown -= 1 * Time.fixedDeltaTime;
        }
        else
        {
            if (isDahsing)
            {
                stopDash = true;
            }

            isDahsing = false;
        }

        if (stopDash)
        {
            rb.velocity = Vector2.zero;
            stopDash = false;
        }
        
    }
}
