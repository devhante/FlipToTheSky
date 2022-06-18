using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.Player.status != PlayerStatus.Dashing)
        {
            GameManager.Instance.PlayerLife--;
        }
    }
}
