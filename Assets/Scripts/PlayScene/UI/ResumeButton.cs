using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class ResumeButton : SimpleButton
    {
        public GameObject pauseMask;

        protected override void OnClick()
        {
            Time.timeScale = 1;
            pauseMask.SetActive(false);
        }
    }
}