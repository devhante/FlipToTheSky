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

        public int Coin
        {
            get; set;
        }

        public int PlayerLife
        {
            get; set;
        }

        public Player Player
        {
            get; private set;
        }

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
            Coin = 0;
            PlayerLife = MaxLife;
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
                BackendManager.Instance.SaveCoin(PlayManager.Instance.Coin, () =>
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