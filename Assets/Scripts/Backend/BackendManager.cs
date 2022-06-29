using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    private void Start()
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공!");
            CustomSignUp();
        }
        else
        {
            Debug.LogError("초기화 실패!");
        }
    }

    public void CustomSignUp()
    {
        string id = "user2";
        string password = "1234";

        var bro = Backend.BMember.CustomSignUp(id, password);
        if (bro.IsSuccess())
        {
            Debug.Log("회원가입 성공!");
        }
        else
        {
            Debug.LogError("회원가입 실패!");
            Debug.LogError(bro);
        }
    }
}
