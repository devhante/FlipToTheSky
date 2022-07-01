using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private void Update()
    {
        // Mobile
        if (Input.touchCount > 0)
        {
            LoadScene();
        }

        // PC
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
