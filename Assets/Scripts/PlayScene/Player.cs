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
        Fliping,
        Flying,
    }

    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb2d;
        private Animator animator;

        [HideInInspector] public PlayerStatus status;
        private readonly float gravityScale = 6;

        private int jumpCount = 2;
        private int jumpPower = 16;

        private readonly float glideSpeed = 2;
        private int glideCount = 0;

        private readonly float dashDuration = 0.2f;
        [HideInInspector] public int dashCount = 3;
        [HideInInspector] public readonly float dashCooldown = 10;
        [HideInInspector] public float dashRemainingCooldown = 10;

        [HideInInspector] public bool hittable = true;

        private int flipSpeed = 10;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            status = PlayerStatus.Running;
        }

        private void Update()
        {
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

        public void OnClickDashButton()
        {
            if (dashCount > 0 && status != PlayerStatus.Dashing)
            {
                Dash();
            }
        }

        private void Jump()
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
            PlayManager.Instance.Speed = PlayManager.Instance.GlideSpeed;
            status = PlayerStatus.Gliding;
            StartCoroutine(GlideCoroutine());
        }

        private void GlideEnd()
        {
            rb2d.gravityScale = gravityScale;
            PlayManager.Instance.Speed = PlayManager.Instance.BaseSpeed;
            status = PlayerStatus.Jumping;
        }

        private IEnumerator GlideCoroutine()
        {
            while (status == PlayerStatus.Gliding)
            {
                transform.Translate(glideSpeed * Time.smoothDeltaTime * Vector3.down);
                yield return null;
            }
        }

        private void Dash()
        {
            StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            dashCount--;
            rb2d.velocity = Vector2.zero;

            rb2d.gravityScale = 0;
            PlayManager.Instance.Speed = PlayManager.Instance.DashSpeed;
            status = PlayerStatus.Dashing;

            yield return new WaitForSeconds(dashDuration);

            rb2d.gravityScale = gravityScale;
            PlayManager.Instance.Speed = PlayManager.Instance.BaseSpeed;
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

        public void Hit()
        {
            if (hittable)
            {
                PlayManager.Instance.PlayerLife--;
                PlayManager.Instance.MainCamera.Vibrate(0.2f, 0.3f);
                PlayManager.Instance.UIController.PlayHitEffect();
                StartCoroutine(HitCoroutine());
            }
        }

        private IEnumerator HitCoroutine()
        {   
            float time = 2;
            float alpha = 0.5f;
            hittable = false;
            var sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

            while (time > 1.5f)
            {
                time -= Time.smoothDeltaTime;
                alpha -= Time.smoothDeltaTime;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                yield return null;
            }

            while (time > 1)
            {
                time -= Time.smoothDeltaTime;
                alpha += Time.smoothDeltaTime;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                yield return null;
            }

            while (time > 0.5f)
            {
                time -= Time.smoothDeltaTime;
                alpha -= Time.smoothDeltaTime;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                yield return null;
            }

            while (time > 0)
            {
                time -= Time.smoothDeltaTime;
                alpha += Time.smoothDeltaTime;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                yield return null;
            }

            alpha = 1;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            yield return new WaitForSeconds(0.5f);

            hittable = true;
        }

        public void Flip()
        {
            if (PlayManager.Instance.Phase == PlayPhase.Flip && status == PlayerStatus.Running)
            {
                StartCoroutine(FlipCoroutine());
            }
        }

        private IEnumerator FlipCoroutine()
        {
            PlayManager.Instance.Speed = 0;
            animator.SetBool("Flip", true);
            status = PlayerStatus.Fliping;
            yield return new WaitForSeconds(0.25f);

            PlayManager.Instance.Speed = PlayManager.Instance.BaseSpeed;
            float startPosY = transform.position.y;
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;

            while (transform.position.y < startPosY + 18)
            {
                transform.Translate(flipSpeed * Time.smoothDeltaTime * Vector3.up);
                PlayManager.Instance.MainCamera.InitialPosition += flipSpeed * Time.smoothDeltaTime * Vector3.up;
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, startPosY + 18, transform.position.z);
            PlayManager.Instance.MainCamera.InitialPosition = new Vector3(PlayManager.Instance.MainCamera.InitialPosition.x, 18, PlayManager.Instance.MainCamera.InitialPosition.z);
            PlayManager.Instance.Phase = PlayPhase.Fly;
            status = PlayerStatus.Flying;
            animator.SetBool("Flip", false);
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