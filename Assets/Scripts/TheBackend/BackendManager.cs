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

        private void Update()
        {
            Backend.AsyncPoll();
        }

        public void InitAndLogin(GameManager.callback callback)
        {
            if (!Backend.IsInitialized)
            {
                Backend.InitializeAsync(true, bro =>
                {
                    if (bro.IsSuccess())
                    {
                        Debug.Log("초기화 성공!");
                        GuestLogin(() =>
                        {
                            callback();
                        });
                    }
                    else
                    {
                        Debug.LogError("초기화 실패!");
                        callback();
                    }
                });
            }
        }

        public void GuestLogin(GameManager.callback callback)
        {
            Backend.BMember.GuestLogin("게스트 로그인으로 로그인함", bro =>
            {
                if (bro.IsSuccess())
                {
                    if (bro.GetStatusCode() == "201")
                        Debug.Log("게스트 회원가입에 성공했습니다.");
                    else if (bro.GetStatusCode() == "200")
                        Debug.Log("게스트 로그인에 성공했습니다.");
                }
                callback();
            });
        }

        public delegate void userInfoCallback(GameManager.UserInformation value);


        public void GetUserInfo(userInfoCallback callback)
        {
            GameManager.UserInformation userInfo;

            Backend.GameData.GetMyData("Coin", new Where(), 1, bro =>
            {
                if (!bro.IsSuccess())
                {
                    Debug.LogError(bro);
                    userInfo.havingCoin = 0;
                    userInfo.earnedCoin = 0;
                }
                else
                {
                    var data = bro.GetFlattenJSON()["rows"];
                    if (data.Count <= 0)
                    {
                        Param param = new Param();
                        param.Add("having", 0);
                        param.Add("earned", 0);
                        Backend.GameData.Insert("Coin", param, bro =>
                        {
                            userInfo.havingCoin = 0;
                            userInfo.earnedCoin = 0;
                            callback(userInfo);
                        });
                    }
                    else
                    {
                        userInfo.havingCoin = (int)data[0]["having"];
                        userInfo.earnedCoin = (int)data[0]["earned"];
                        callback(userInfo);
                    }
                }
            });
        }

        public void SaveCoin(int value, GameManager.callback callback)
        {
            int having = 0;
            int earned = 0;
            Backend.GameData.GetMyData("Coin", new Where(), 1, bro =>
            {
                if (!bro.IsSuccess())
                {
                    Debug.LogError(bro);
                    return;
                }

                void UpdateCoin()
                {
                    having += value;
                    earned += value;

                    Param param2 = new Param();
                    param2.Add("having", having);
                    param2.Add("earned", earned);
                    Backend.GameData.Update("Coin", new Where(), param2, bro =>
                    {
                        callback();
                    });
                }

                var data = bro.GetFlattenJSON()["rows"];
                if (data.Count <= 0)
                {
                    Param param1 = new Param();
                    param1.Add("having", 0);
                    param1.Add("earned", 0);
                    Backend.GameData.Insert("Coin", param1, bro =>
                    {
                        UpdateCoin();
                    });
                }
                else
                {
                    having = (int)data[0]["having"];
                    earned = (int)data[0]["earned"];

                    UpdateCoin();
                }
            });
        }

        public void UseCoin(int value, GameManager.callback callback)
        {
            int having = 0;

            Backend.GameData.GetMyData("Coin", new Where(), 1, bro =>
            {
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
                    Backend.GameData.Insert("Coin", param1, bro =>
                    {
                        callback();
                    });
                }
                else
                {
                    having = (int)data[0]["having"] - value;
                    if (having < 0)
                    {
                        callback();
                    }
                    else
                    {
                        Param param2 = new Param();
                        param2.Add("having", having);
                        Backend.GameData.Update("Coin", new Where(), param2, bro =>
                        {
                            callback();
                        });
                    }
                }
            });
        }
    }
}