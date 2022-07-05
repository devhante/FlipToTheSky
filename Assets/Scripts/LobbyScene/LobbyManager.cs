using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.LobbyScene
{
    public class LobbyManager : MonoBehaviour
    {
        private static LobbyManager instance = null;

        public static LobbyManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            if (instance)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }
    }
}