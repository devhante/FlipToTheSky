using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class Life : MonoBehaviour
    {
        private Image imageComponent;

        public Sprite filledLifeSprite;
        public Sprite emptyLifeSprite;

        public int Index { get; set; }

        private void Awake()
        {
            imageComponent = GetComponent<Image>();
        }

        private void Update()
        {
            imageComponent.sprite = PlaySceneManager.Instance.PlayerLife > Index ? filledLifeSprite : emptyLifeSprite;
        }
    }
}