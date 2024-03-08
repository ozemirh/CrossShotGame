using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    GameObject startPoint; // Assign the start position GameObject
    public LayerMask collisionLayer; // Choose the layer(s) to detect collisions with
    public float toggleInterval = 1.0f; // Time interval in seconds
    private bool isLineVisible = true;
    public bool shouldToggle = false;
    private LineRenderer lineRenderer;
    public bool rotate = false;
    Vector3 startPos;
    Vector3 endPos;
    public float minRotation;
    public float maxRotation;
    public float duration;
    Ground ground;
    public UnityEngine.Rendering.Universal.Light2D light2D;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(ToggleLineVisibility());
        if (rotate)
        {
            StartCoroutine(RotateZ());
        }
        startPoint = transform.GetChild(0).gameObject;
        ground = Camera.main.GetComponent<CameraController>().GetGround();
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
        startPos = startPoint.transform.position;
        DrawLine();
    }
    IEnumerator ToggleLineVisibility()
    {
        while (shouldToggle)
        {
            isLineVisible = !isLineVisible;
            lineRenderer.enabled = isLineVisible;
            if (light2D)
            {
                light2D.gameObject.SetActive(isLineVisible);
            }
            yield return new WaitForSeconds(toggleInterval);
        }
    }
    void DrawLine()
    {
        Vector3 direction = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.right;
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction);
        if (hit.collider != null)
        {
            endPos = hit.point;
            if (hit.collider.gameObject.tag == "Ball" && isLineVisible)
            {
                ground.BallHit();
            }
        }
        else
        {
            endPos = startPos + direction * 100; // You can adjust the length of the line here.
        }
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}





