using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    Running,
    Jumping
}

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    private int jumpCount;
    private int jumpPower;
    private float gravityScale = 6;
    private float fallingGravityScale = 7;

    private PlayerStatus status;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = 2;
        jumpPower = 16;
        status = PlayerStatus.Running;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (status == PlayerStatus.Jumping)
        {
            if (rb2d.velocity.y >= 0)
            {
                rb2d.gravityScale = gravityScale;
            }
            else if (rb2d.velocity.y < 0)
            {
                rb2d.gravityScale = fallingGravityScale;
            }
        }
    }

    private void Jump()
    {
        if (jumpCount > 0)
        {
            jumpCount--;
            animator.SetBool("Jump", true);
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            status = PlayerStatus.Jumping;
        }
    }

    private void Land()
    {
        if (status == PlayerStatus.Jumping)
        {
            animator.SetBool("Jump", false);
        }
        
        jumpCount = 2;
        status = PlayerStatus.Running;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Land();
        }
    }
}
