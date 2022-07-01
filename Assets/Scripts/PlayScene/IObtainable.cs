using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public interface IObtainable
    {
        void OnTriggerEnter2D(Collider2D collision);
        void Obtained(Collider2D collision);
    }
}

