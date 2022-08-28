using FTS.TheBackend;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class ExitButton : SimpleButton
    {
        protected override void OnClick()
        {
            Time.timeScale = 1;
            GameManager.Instance.LoadScene("LobbyScene", (callback) =>
            {
                BackendManager.Instance.SaveCoin(PlaySceneManager.Instance.Dreampiece, () =>
                {
                    GameManager.Instance.UpdateUserInfo(() =>
                    {
                        callback();
                    });
                });
            });
        }
    }
}