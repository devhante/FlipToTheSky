using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Background : MonoBehaviour
    {
        private void Update()
        {
            if (transform.position.x < -64)
            {
                transform.Translate(new Vector3(192, 0, 0));
            }
            else
            {
                transform.Translate(PlayManager.Instance.Speed * 0.2f * Time.smoothDeltaTime * Vector3.left);
            }
        }
    }
}