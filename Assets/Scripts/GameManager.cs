using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Speed = 7;
        Coin = 0;
        PlayerLife = 3;
        Player = player;
    }
}
