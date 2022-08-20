using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTS.PlayScene
{
    public class Block : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Player.Instance.Hit();
            }
        }
    }
}