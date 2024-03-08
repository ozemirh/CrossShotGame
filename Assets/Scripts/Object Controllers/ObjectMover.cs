using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float speed = 2.0f;

    private float startTime;
    private float journeyLength;

    private void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition.position, endPosition.position);
    }

    private void Update()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        if (fractionOfJourney < 1.0f)
        {
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, fractionOfJourney);
        }
        else
        {
            // Swap start and end positions to create a loop
            Transform temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
            startTime = Time.time;
        }
    }
}

