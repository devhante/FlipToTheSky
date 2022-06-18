using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x < -64)
        {
            Destroy(transform.gameObject);
        }

        transform.Translate(GameManager.Instance.Speed * Time.smoothDeltaTime * Vector3.left);
    }
}
