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
            Debug.Log("�ʱ�ȭ ����!");
            CustomSignUp();
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ����!");
        }
    }

    public void CustomSignUp()
    {
        string id = "user2";
        string password = "1234";

        var bro = Backend.BMember.CustomSignUp(id, password);
        if (bro.IsSuccess())
        {
            Debug.Log("ȸ������ ����!");
        }
        else
        {
            Debug.LogError("ȸ������ ����!");
            Debug.LogError(bro);
        }
    }
}
