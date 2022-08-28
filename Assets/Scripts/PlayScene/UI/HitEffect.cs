using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    private Image[] images;

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(HitEffectCoroutine());
    }

    private IEnumerator HitEffectCoroutine()
    {
        foreach (var item in images)
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1);
        yield return new WaitForSeconds(0.2f);

        float time = 1;
        float alpha = 1;
        while (time > 0)
        {
            time -= Time.smoothDeltaTime;
            alpha -= Time.smoothDeltaTime;
            foreach (var item in images)
                item.color = new Color(item.color.r, item.color.g, item.color.b, alpha);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
