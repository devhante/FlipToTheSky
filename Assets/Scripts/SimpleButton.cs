using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS
{
    public abstract class SimpleButton : MonoBehaviour
    {
        protected Button buttonComponent;

        protected void Awake()
        {
            buttonComponent = GetComponent<Button>();
            buttonComponent.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}