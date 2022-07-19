using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class FlyingBlockV : Block
    {
        private void Update()
        {
            if (transform.position.y > 32)
            {
                Destroy(transform.gameObject);
            }

            transform.Translate(PlayManager.Instance.Speed * Time.smoothDeltaTime * Vector3.up);
        }
    }
}