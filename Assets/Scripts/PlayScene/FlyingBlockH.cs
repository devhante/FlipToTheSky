using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class FlyingBlockH : Block
    {
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