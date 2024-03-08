using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float minRotation = 0f;
    public float maxRotation = 180f;
    public float duration = 2f;
    public float sawRotationSpeed = 20f;
    public GameObject saw;
    public Transform sawPos;

    void Start()
    {
        StartCoroutine(RotateZ());
    }

    IEnumerator RotateZ()
    {
        while (true)
        {
            yield return RotateFromTo(minRotation, maxRotation, duration);
            yield return RotateFromTo(maxRotation, minRotation, duration);
        }
    }

    IEnumerator RotateFromTo(float fromAngle, float toAngle, float time)
    {
        float startAngle = transform.localEulerAngles.z;
        float endAngle = startAngle + Mathf.DeltaAngle(startAngle, toAngle);
        for (float t = 0f; t < time; t += Time.deltaTime)
        {
            float angle = Mathf.LerpAngle(fromAngle, toAngle, t / time);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
            yield return null;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, endAngle);
    }
    void Update()
    {
        if (saw != null)
        {
            saw.transform.position = sawPos.position;
            saw.transform.Rotate(new Vector3(0, 0, sawRotationSpeed * Time.deltaTime));
        }
    }
}
