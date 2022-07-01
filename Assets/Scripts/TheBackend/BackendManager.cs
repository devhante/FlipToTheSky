using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

namespace FTS.TheBackend
{
    public class BackendManager : MonoBehaviour
    {
        private static BackendManager instance = null;

        public static BackendManager Instance
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

        private void Start()
        {
            var bro = Backend.Initialize(true);
            if (bro.IsSuccess())
            {
                Debug.Log("�ʱ�ȭ ����!");
                GuestLogin();
            }
            else
            {
                Debug.LogError("�ʱ�ȭ ����!");
            }
        }

        public void GuestLogin()
        {
            var bro = Backend.BMember.GuestLogin("�Խ�Ʈ �α������� �α�����");
            if (bro.IsSuccess())
            {
                if (bro.GetStatusCode() == "201")
                    Debug.Log("�Խ�Ʈ ȸ�����Կ� �����߽��ϴ�.");
                else if (bro.GetStatusCode() == "200")
                    Debug.Log("�Խ�Ʈ �α��ο� �����߽��ϴ�.");
            }
        }

        public int GetCoin()
        {
            var bro = Backend.GameData.GetMyData("Coin", new Where(), 1);

            if (!bro.IsSuccess())
            {
                Debug.LogError(bro);
                return 0;
            }

            var data = bro.GetFlattenJSON()["rows"];
            if (data.Count <= 0)
            {
                Param param = new Param();
                param.Add("having", 0);
                param.Add("earned", 0);
                var result = Backend.GameData.Insert("Coin", param);
                Debug.Log(result);
                return 0;
            }
            else
            {
                return (int)data[0]["having"];
            }
        }

        public void SaveCoin(int value)
        {
            int having = 0;
            int earned = 0;
            var bro = Backend.GameData.GetMyData("Coin", new Where(), 1);

            if (!bro.IsSuccess())
            {
                Debug.LogError(bro);
                return;
            }

            var data = bro.GetFlattenJSON()["rows"];
            if (data.Count <= 0)
            {
                Param param1 = new Param();
                param1.Add("having", 0);
                param1.Add("earned", 0);
                Backend.GameData.Insert("Coin", param1);
            }
            else
            {
                having = (int)data[0]["having"];
                earned = (int)data[0]["earned"];
            }

            having += value;
            earned += value;

            Param param2 = new Param();
            param2.Add("having", having);
            param2.Add("earned", earned);
            Backend.GameData.Update("Coin", new Where(), param2);
        }

        public void UseCoin(int value)
        {
            int having = 0;
            var bro = Backend.GameData.GetMyData("Coin", new Where(), 1);

            if (!bro.IsSuccess())
            {
                Debug.LogError(bro);
                return;
            }

            var data = bro.GetFlattenJSON()["rows"];
            if (data.Count <= 0)
            {
                Param param1 = new Param();
                param1.Add("having", 0);
                Backend.GameData.Insert("Coin", param1);
            }
            else
            {
                having = (int)data[0]["having"];
            }

            having -= value;

            Param param2 = new Param();
            param2.Add("having", having);
            Backend.GameData.Update("Coin", new Where(), param2);
        }
    }
}