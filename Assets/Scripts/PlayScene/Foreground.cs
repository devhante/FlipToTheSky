using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Foreground : MonoBehaviour
    {
        private void Update()
        {
            if (transform.position.x < -64)
            {
                transform.Translate(new Vector3(162, 0, 0));
            }
            else
            {
                transform.Translate(Player.Instance.MoveSpeed * 0.5f * Time.smoothDeltaTime * Vector3.left);
            }
        }
    }
}