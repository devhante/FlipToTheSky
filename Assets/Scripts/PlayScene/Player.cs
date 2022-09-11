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
        private static Player instance = null;

        public static Player Instance
        {
            get
            {
                return instance;
            }
        }

        public readonly float BaseSpeed = 8;
        public readonly float GlideSpeed = 10;
        public readonly float DashSpeed = 18;
        public readonly float FlipSpeed = 10;
        public readonly int MaxDashCount = 3;
        public readonly float DashCooldown = 10;

        private Rigidbody2D rb2d;
        private Animator animator;
        
        private readonly float gravityScale = 6;
        private readonly int jumpPower = 16;
        private readonly float glideSpeed = 2;
        private readonly float dashDuration = 0.2f;

        private int jumpCount = 2;
        private int glideCount = 0;

        public int DashCount { get; set; }
        public float RemainingDashCooldown { get; set; }
        public PlayerStatus Status { get; set; }
        public float MoveSpeed { get; set; }
        public bool Hittable { get; set; }

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            DashCount = MaxDashCount;
            RemainingDashCooldown = DashCooldown;
            Status = PlayerStatus.Running;
            MoveSpeed = BaseSpeed;
            Hittable = true;

            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (DashCount < 3)
            {
                RemainingDashCooldown = Mathf.Max(0, RemainingDashCooldown - Time.deltaTime);
                if (RemainingDashCooldown == 0)
                {
                    DashCount++;
                    RemainingDashCooldown = DashCooldown;
                }
            }
        }

        public void OnClickDashButton()
        {
            if (DashCount > 0 && Status != PlayerStatus.Dashing)
            {
                Dash();
            }
        }

        public void Jump()
        {
            if (jumpCount > 0)
            {
                jumpCount--;
                if (Status == PlayerStatus.Running)
                {
                    glideCount = 1;
                }

                animator.SetBool("Jump", true);
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Status = PlayerStatus.Jumping;
            }
        }

        public void StartGliding()
        {
            if (jumpCount == 0 && glideCount > 0 && Status == PlayerStatus.Jumping && rb2d.velocity.y < 0)
            {
                glideCount--;
                rb2d.velocity = Vector2.zero;
                rb2d.gravityScale = 0;
                MoveSpeed = GlideSpeed;
                Status = PlayerStatus.Gliding;
                StartCoroutine(GlideCoroutine());
            }
        }

        public void FinishGliding()
        {
            if (Status == PlayerStatus.Gliding)
            {
                rb2d.gravityScale = gravityScale;
                MoveSpeed = BaseSpeed;
                Status = PlayerStatus.Jumping;
            }
        }

        private IEnumerator GlideCoroutine()
        {
            while (Status == PlayerStatus.Gliding)
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
            Hittable = false;
            DashCount--;
            rb2d.velocity = Vector2.zero;

            rb2d.gravityScale = 0;
            MoveSpeed = DashSpeed;
            Status = PlayerStatus.Dashing;

            yield return new WaitForSeconds(dashDuration);

            rb2d.gravityScale = gravityScale;
            MoveSpeed = BaseSpeed;
            Status = PlayerStatus.Jumping;
            Hittable = true;
        }

        private void Land()
        {
            if (Status == PlayerStatus.Jumping)
            {
                animator.SetBool("Jump", false);
            }

            if (Status == PlayerStatus.Gliding)
            {
                FinishGliding();
                animator.SetBool("Jump", false);
            }

            jumpCount = 2;
            glideCount = 0;
            Status = PlayerStatus.Running;
        }

        public void Hit()
        {
            if (Hittable)
            {
                PlaySceneManager.Instance.PlayerLife--;
                MainCamera.Instance.Vibrate(0.2f, 0.3f);
                UIController.Instance.PlayHitEffect();
                StartCoroutine(HitCoroutine());
            }
        }

        private IEnumerator HitCoroutine()
        {   
            float time = 2;
            float alpha = 0.5f;
            Hittable = false;
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

            Hittable = true;
        }

        public void EnterFlipPhase()
        {
            if (Status == PlayerStatus.Gliding)
            {
                rb2d.velocity = Vector2.zero;
                rb2d.gravityScale = gravityScale;
            }
        }

        public void Flip()
        {
            if (PlaySceneManager.Instance.Phase == GamePhase.Flip && Status == PlayerStatus.Running)
            {
                StartCoroutine(FlipCoroutine());
            }
        }

        private IEnumerator FlipCoroutine()
        {
            MoveSpeed = 0;
            animator.SetBool("Flip", true);
            Status = PlayerStatus.Fliping;
            yield return new WaitForSeconds(0.25f);

            MoveSpeed = BaseSpeed;
            float startPosY = transform.position.y;
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;

            while (transform.position.y < startPosY + 18)
            {
                transform.Translate(FlipSpeed * Time.smoothDeltaTime * Vector3.up);
                MainCamera.Instance.InitialPosition += FlipSpeed * Time.smoothDeltaTime * Vector3.up;
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, startPosY + 18, transform.position.z);
            MainCamera.Instance.InitialPosition = new Vector3(MainCamera.Instance.InitialPosition.x, 18, MainCamera.Instance.InitialPosition.z);
            PlaySceneManager.Instance.Phase = GamePhase.Fly;
            Status = PlayerStatus.Flying;
            animator.SetBool("Flip", false);
        }

        public void Move(Vector3 direction)
        {

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