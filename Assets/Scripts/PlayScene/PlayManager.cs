using System.Collections;
using UnityEngine;
using FTS.TheBackend;

namespace FTS.PlayScene
{
    public enum PlayPhase
    {
        Run,
        Flip,
        Fly,
        Land
    }

    public class PlayManager : MonoBehaviour
    {
        private static PlayManager instance = null;

        public readonly float BaseSpeed = 8;
        public readonly float GlideSpeed = 10;
        public readonly float DashSpeed = 18;
        public readonly int MaxLife = 3;

        public static PlayManager Instance
        {
            get
            {
                return instance;
            }
        }

        public PlayPhase Phase
        {
            get; set;
        }

        public float Speed
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

        public UIController UIController
        {
            get; private set;
        }

        public Player Player
        {
            get; private set;
        }

        [SerializeField] private MainCamera mainCamera;
        [SerializeField] private UIController uiController;
        [SerializeField] private Player player;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            Phase = PlayPhase.Run;
            Speed = BaseSpeed;
            Dreampiece = 0;
            PlayerLife = MaxLife;
            MainCamera = mainCamera;
            UIController = uiController;
            Player = player;
        }

        private void Update()
        {
            if (PlayerLife <= 0)
            {
                GameOver();
            }
        }

        public void EnterFlipPhase()
        {
            if (Phase == PlayPhase.Run)
            {
                Phase = PlayPhase.Flip;
                StartCoroutine(FlipPhaseCoroutine());
            }
        }

        private IEnumerator FlipPhaseCoroutine()
        {
            UIController.flipTitle.SetActive(true);
            UIController.dashButton.gameObject.SetActive(false);
            UIController.jumpButton.gameObject.SetActive(false);
            UIController.warnings.ShowWarning(2, 0, 8);
            UIController.warnings.ShowWarning(3, 0, 8);
            UIController.warnings.ShowWarning(4, 0, 8);
            UIController.warnings.ShowWarning(5, 0, 8);
            UIController.warnings.ShowWarning(6, 0, 8);
            yield return new WaitForSeconds(3);
            UIController.flipTimer.gameObject.SetActive(true);
            UIController.flipTimer.StartTimer();

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