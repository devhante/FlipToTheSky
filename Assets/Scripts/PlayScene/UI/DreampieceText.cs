using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FTS.PlayScene
{
    public class DreampieceText : MonoBehaviour
    {
        private TMP_Text textComponent;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            textComponent.text = PlaySceneManager.Instance.Dreampiece.ToString();
        }
    }
}