using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coin : MonoBehaviour, IObtainable
{
    public void Obtained(Collider2D collision)
    {
        GameManager.Instance.Coin++;
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
