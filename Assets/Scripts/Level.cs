using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(GameManager.Instance.Speed * Time.smoothDeltaTime * Vector3.left);
    }
}
