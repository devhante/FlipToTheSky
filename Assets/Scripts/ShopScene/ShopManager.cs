using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.ShopScene
{
    public class ShopManager : MonoBehaviour
    {
        private static ShopManager instance = null;

        public readonly int pickOnePrice = 500;
        public readonly int pickFivePrice = 2400;

        public static ShopManager Instance
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