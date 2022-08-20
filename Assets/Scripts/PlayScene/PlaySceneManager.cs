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

        public readonly int MaxLife = 3;

        public static PlaySceneManager Instance
        {
            get
            {
                return instance;
            }
        }

        public GamePhase Phase
        {
            get; set;
        }

        public int Dreampiece
        {
            get; set;
        }

        public int PlayerLife
        {
            get; set;
        }

        public MainCamera MainCamera
        {
            get; private set;
        }

        [SerializeField] private MainCamera mainCamera;

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
            MainCamera = mainCamera;
        }

        private void Update()
        {
            if (PlayerLife <= 0)
            {
                GameOver();
            }
        }

        public void EnterPhase(GamePhase phase)
        {
            if (phase == GamePhase.Flip)
            {
                Phase = GamePhase.Flip;
                Player.Instance.EnterFlipPhase();
                StartCoroutine(FlipPhaseCoroutine());
            }
            if (phase == GamePhase.Run)
            {
                Phase = GamePhase.Run;
            }
        }

        private IEnumerator FlipPhaseCoroutine()
        {
            StartCoroutine(FlipTouchCoroutine());
            UIController.Instance.flipTitle.SetActive(true);
            UIController.Instance.dashButton.gameObject.SetActive(false);
            UIController.Instance.jumpButton.gameObject.SetActive(false);

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
                Debug.Log("new WaitForSeconds(" + UIController.Instance.flipTimer.TimeLimit + ")");
                yield return new WaitForSeconds(UIController.Instance.flipTimer.TimeLimit);

                if (Player.Instance.Status == PlayerStatus.Running)
                {
                    UIController.Instance.flipTitle.SetActive(false);
                    UIController.Instance.flipTimer.gameObject.SetActive(false);
                    UIController.Instance.dashButton.gameObject.SetActive(true);
                    UIController.Instance.jumpButton.gameObject.SetActive(true);
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