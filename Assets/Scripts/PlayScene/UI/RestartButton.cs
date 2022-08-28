using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class RestartButton : SimpleButton
    {
        protected override void OnClick()
        {
            Time.timeScale = 1;
            GameManager.Instance.LoadScene("PlayScene", (callback) =>
            {
                callback();
            });
        }
    }
}