using System.Collections;
using UnityEngine;
using FTS.TheBackend;

namespace FTS.PlayScene
{
    public enum GamePhase
    {
        Run,
        Flip,
        Fly,
        Land
    }

    public class PlaySceneManager : MonoBehaviour
    {
        private static PlaySceneManager instance = null;

        public static PlaySceneManager Instance
        {
            get
            {
                return instance;
            }
        }

        public readonly int MaxLife = 3;

        public GamePhase Phase { get; set; }
        public int Dreampiece { get; set; }

        public int PlayerLife{ get; set; }

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            Phase = GamePhase.Run;
            Dreampiece = 0;
            PlayerLife = MaxLife;
        }

        private void Update()
        {
            if (PlayerLife <= 0)
            {
                GameOver();
            }
            Debug.Log(Phase);
        }

        public void EnterPhase(GamePhase phase)
        {
            if (phase == GamePhase.Run)
            {
                Phase = GamePhase.Run;
            }
            if (phase == GamePhase.Flip)
            {
                Phase = GamePhase.Flip;
                Player.Instance.EnterFlipPhase();
                StartCoroutine(FlipPhaseCoroutine());
            }
            if (phase == GamePhase.Fly)
            {
                Phase = GamePhase.Fly;
            }
            if (phase == GamePhase.Land)
            {
                Phase = GamePhase.Land;
                Player.Instance.EnterLandPhase();
            }
        }

        private IEnumerator FlipPhaseCoroutine()
        {
            StartCoroutine(FlipTouchCoroutine());
            UIController.Instance.flipTitle.SetActive(true);

            FlyingBlock[] flyingBlocks = new FlyingBlock[5];

            for (var i = 0; i < 5; i++)
            {
                flyingBlocks[i] = FlyingBlockSpawner.Instance.SpawnHorizontalBlock(i + 1, 8);
            }
            yield return new WaitForSeconds(3);

            if (Player.Instance.Status == PlayerStatus.Running)
            {
                UIController.Instance.flipTimer.gameObject.SetActive(true);
                UIController.Instance.flipTimer.StartTimer();
                yield return new WaitForSeconds(UIController.Instance.flipTimer.TimeLimit);

                if (Player.Instance.Status == PlayerStatus.Running)
                {
                    UIController.Instance.flipTitle.SetActive(false);
                    UIController.Instance.flipTimer.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator FlipTouchCoroutine()
        {
            bool flipped = false;
            Vector2 startTouchPos = Vector2.zero;
            Vector2 endTouchPos;
            
            while (!flipped)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        startTouchPos = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        endTouchPos = touch.position;
                        if (endTouchPos.y > startTouchPos.y)
                        {
                            flipped = true;
                            int childCount = FlyingBlockSpawner.Instance.transform.childCount;
                            for (int i = 0; i < childCount; i++)
                            {
                                FlyingBlockSpawner.Instance.transform.GetChild(i).GetComponent<FlyingBlock>().Destroy();
                            }
                            UIController.Instance.flipTitle.SetActive(false);
                            UIController.Instance.flipTimer.gameObject.SetActive(false);
                            Player.Instance.Flip();
                        }
                    }
                }

                // TEST
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    flipped = true;
                    int childCount = FlyingBlockSpawner.Instance.transform.childCount;
                    for (int i = 0; i < childCount; i++)
                    {
                        FlyingBlockSpawner.Instance.transform.GetChild(i).GetComponent<FlyingBlock>().Destroy();
                    }
                    UIController.Instance.flipTitle.SetActive(false);
                    UIController.Instance.flipTimer.gameObject.SetActive(false);
                    Player.Instance.Flip();
                }

                yield return null;
            }
        }

        public void GameOver()
        {
            GameManager.Instance.LoadScene("LobbyScene", (callback) =>
            {
                BackendManager.Instance.SaveCoin(Dreampiece, () =>
                {
                    GameManager.Instance.UpdateUserInfo(() =>
                    {
                        callback();
                    });
                });
            });
        }
    }
}