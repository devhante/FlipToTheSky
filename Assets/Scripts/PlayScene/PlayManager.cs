using UnityEngine;
using FTS.TheBackend;

namespace FTS.PlayScene
{
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