using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObscuringItem : MonoBehaviour
{
    private const float fadeInSeconds = 0.25f;
    private const float fadeOutSeconds = 0.35f;
    private const float targetAlpha = 0.45f;
    private SpriteRenderer sprite;


    private void Awake()
    {
        sprite= gameObject.GetComponent<SpriteRenderer>();
    }



   public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }


    IEnumerator FadeOutRoutine()
    {
        float currentAlpha = sprite.color.a;
        float distance = currentAlpha - targetAlpha;

        while (currentAlpha-targetAlpha > 0.01f)
        {
            currentAlpha = currentAlpha - distance / fadeOutSeconds * Time.deltaTime;
            sprite.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
       
        sprite.color = new Color(1f, 1f, 1f, targetAlpha);
    }
    IEnumerator FadeInRoutine()
    {
        float currentAlpha = sprite.color.a;
        float distance = 1f - currentAlpha;

        while (1f - currentAlpha > 0.01f)
        {
            currentAlpha = currentAlpha + distance / fadeInSeconds * Time.deltaTime;
            sprite.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
