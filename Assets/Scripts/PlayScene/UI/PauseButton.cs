using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class PauseButton : SimpleButton
    {
        public GameObject pauseMask;

        protected override void OnClick()
        {
            Time.timeScale = 0;
            pauseMask.SetActive(true);
        }
    }
}