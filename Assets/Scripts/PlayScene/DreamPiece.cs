using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FTS.PlayScene
{
    public class DreamPiece : MonoBehaviour, IObtainable
    {
        public void Obtained(Collider2D collision)
        {
            PlaySceneManager.Instance.Dreampiece++;
            Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Obtained(collision);
            }
        }
    }
}