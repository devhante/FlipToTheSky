using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Level : MonoBehaviour
    {
        public GamePhase phase;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (PlaySceneManager.Instance.Phase != phase)
                {
                    PlaySceneManager.Instance.EnterPhase(phase);
                }
            }
        }

        private void Update()
        {
            if (transform.position.x < -200)
            {
                Destroy(transform.gameObject);
            }

            transform.Translate(Player.Instance.MoveSpeed * Time.smoothDeltaTime * Vector3.left);
        }
    }
}