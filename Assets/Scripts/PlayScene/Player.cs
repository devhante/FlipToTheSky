using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public enum PlayerStatus
    {
        Running,
        Jumping,
        Gliding,
        Dashing,
    }

    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb2d;
        private Animator animator;

        private int jumpCount;
        private int jumpPower;
        private int glideCount;
        [HideInInspector] public int dashCount;
        [HideInInspector] public float dashCooldown;
        [HideInInspector] public float dashRemainingCooldown;

        private readonly float gravityScale = 6;
        private readonly float fallingGravityScale = 6;
        private readonly float glideSpeed = 2;

        [HideInInspector] public PlayerStatus status;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            jumpCount = 2;
            jumpPower = 16;
            glideCount = 0;
            dashCount = 3;
            dashCooldown = 10;
            dashRemainingCooldown = 10;
            status = PlayerStatus.Running;
        }

        private void Update()
        {
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

            if (status == PlayerStatus.Gliding)
            {
                transform.Translate(glideSpeed * Time.smoothDeltaTime * Vector3.down);
                PlayManager.Instance.Speed = PlayManager.Instance.GlideSpeed;
            }
            else if (status == PlayerStatus.Dashing)
            {
                PlayManager.Instance.Speed = PlayManager.Instance.DashSpeed;
            }
            else
            {
                PlayManager.Instance.Speed = PlayManager.Instance.BaseSpeed;
            }

            if (dashCount < 3)
            {
                dashRemainingCooldown = Mathf.Max(0, dashRemainingCooldown - Time.deltaTime);
                if (dashRemainingCooldown == 0)
                {
                    dashCount++;
                    dashRemainingCooldown = dashCooldown;
                }
            }
        }

        public void OnPressJumpButton()
        {
            if (jumpCount > 0)
            {
                Jump();
            }
        }

        public void OnHoldJumpButton()
        {
            if (jumpCount == 0 && glideCount > 0 && status == PlayerStatus.Jumping && rb2d.velocity.y < 0)
            {
                GlideStart();
            }
        }

        public void OnReleaseJumpButton()
        {
            if (status == PlayerStatus.Gliding)
            {
                GlideEnd();
            }
        }

        public void Jump()
        {
            jumpCount--;
            if (status == PlayerStatus.Running)
            {
                glideCount = 1;
            }

            animator.SetBool("Jump", true);
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            status = PlayerStatus.Jumping;
        }

        private void GlideStart()
        {
            glideCount--;
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;
            status = PlayerStatus.Gliding;
        }

        private void GlideEnd()
        {
            rb2d.gravityScale = fallingGravityScale;
            status = PlayerStatus.Jumping;
        }

        public void Dash()
        {
            if (dashCount > 0 && status != PlayerStatus.Dashing)
            {
                StartCoroutine(DashCoroutine());
            }
        }

        private IEnumerator DashCoroutine()
        {
            dashCount--;
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;
            status = PlayerStatus.Dashing;
            yield return new WaitForSeconds(0.2f);
            rb2d.gravityScale = fallingGravityScale;
            status = PlayerStatus.Jumping;
        }

        private void Land()
        {
            if (status == PlayerStatus.Jumping)
            {
                animator.SetBool("Jump", false);
            }

            if (status == PlayerStatus.Gliding)
            {
                GlideEnd();
                animator.SetBool("Jump", false);
            }

            jumpCount = 2;
            glideCount = 0;
            status = PlayerStatus.Running;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Land();
            }
        }
    }
}