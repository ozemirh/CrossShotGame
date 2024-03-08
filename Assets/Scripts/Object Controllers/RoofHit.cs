using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofHit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(AnimateRoof(0, 255, 0.3f));
    }
    IEnumerator AnimateRoof(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;
        float aValue = 150;
        while (elapsed < duration)
        {
            aValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            spriteRenderer.color = new Color32(0, 0, 0, (byte)aValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color32(0, 0, 0, (byte)aValue);
        StartCoroutine(FadeRoof(255, 0, 0.3f));
    }
    IEnumerator FadeRoof(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;
        float aValue = 0;
        while (elapsed < duration)
        {
            aValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            spriteRenderer.color = new Color32(0, 0, 0, (byte)aValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color32(0, 0, 0, (byte)aValue);
    }
}
