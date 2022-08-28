using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class UIController : MonoBehaviour
    {
        private static UIController instance = null;

        public static UIController Instance
        {
            get
            {
                return instance;
            }
        }

        public GameObject jumpButton;
        public GameObject dashButton;
        public GameObject pauseMask;
        public GameObject hitEffect;

        public GameObject flipTitle;
        public FlipTimer flipTimer;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        private void Update()
        {
            jumpButton.SetActive(PlaySceneManager.Instance.Phase == GamePhase.Run);
            dashButton.SetActive(PlaySceneManager.Instance.Phase == GamePhase.Run);
        }

        public void PlayHitEffect()
        {
            hitEffect.SetActive(true);
        }

        public GameObject[] rightWarnings;
        public GameObject[] downWarnings;

        public GameObject GetRightWarning(int index)
        {
            if (index < rightWarnings.Length)
                return rightWarnings[index];
            else
                return null;
        }

        public GameObject GetDownWarning(int index)
        {
            if (index < downWarnings.Length)
                return downWarnings[index];
            else
                return null;
        }
    }
}