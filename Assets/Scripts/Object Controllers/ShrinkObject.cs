using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkObject : MonoBehaviour
{
    private Vector3 startScale;
    public Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
    public float duration = 2f;  // Duration of the scaling animation in seconds

    private void Start()
    {
        startScale = transform.localScale;

        // Start the scaling coroutine
        StartCoroutine(ScaleObjectOverTime());
    }

    private IEnumerator ScaleObjectOverTime()
    {
        while (true)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Swap start and target scales for the next iteration
            Vector3 temp = startScale;
            startScale = targetScale;
            targetScale = temp;
        }
    }
}
