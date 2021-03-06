using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Level : MonoBehaviour
    {
        public PlayPhase phase;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (PlayManager.Instance.Phase != phase)
                {
                    PlayManager.Instance.EnterPhase(phase);
                }
            }
        }

        private void Update()
        {
            if (transform.position.x < -64)
            {
                Destroy(transform.gameObject);
            }

            transform.Translate(PlayManager.Instance.Speed * Time.smoothDeltaTime * Vector3.left);
        }
    }
}