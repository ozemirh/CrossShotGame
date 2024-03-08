using System.Collections;
using UnityEngine;


public class SawMove : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float moveSpeed = 5.0f;
    public float slowdownDistance = 1.0f;
    public float loopDelay = 1.0f; // Time to pause at the end position before looping.

    private float distance;
    private float currentLerpTime;
    private float totalLerpTime;

    private void Start()
    {
        // Calculate the initial distance between start and end positions.
        distance = Vector3.Distance(startPosition.position, endPosition.position);

        // Calculate the total time it will take to move between positions at the given speed.
        totalLerpTime = distance / moveSpeed;

        // Initialize the current lerp time.
        currentLerpTime = 0f;

        // Start the movement.
        StartCoroutine(MoveInLoop());
    }

    private IEnumerator MoveInLoop()
    {
        while (true) // Infinite loop
        {
            // Reset the lerp time.
            currentLerpTime = 0f;

            // Move from start to end position.
            while (currentLerpTime < totalLerpTime)
            {
                // Increment the current lerp time.
                currentLerpTime += Time.deltaTime;

                // Calculate the interpolation value between 0 and 1.
                float t = currentLerpTime / totalLerpTime;

                // Apply a slowdown factor when approaching the end positions.
                if (t < slowdownDistance)
                {
                    t = Mathf.Pow(t / slowdownDistance, 2); // You can adjust this power to control the slowdown rate.
                }
                else if (t > 1.0f - slowdownDistance)
                {
                    t = 1.0f - Mathf.Pow((1.0f - t) / slowdownDistance, 2);
                }

                // Use Vector3.Lerp to move the object.
                transform.position = Vector3.Lerp(startPosition.position, endPosition.position, t);

                yield return null;
            }

            // Delay at the end position before reversing.
            yield return new WaitForSeconds(loopDelay);

            // Reset the lerp time.
            currentLerpTime = 0f;

            // Move from end to start position.
            while (currentLerpTime < totalLerpTime)
            {
                // Increment the current lerp time.
                currentLerpTime += Time.deltaTime;

                // Calculate the interpolation value between 0 and 1.
                float t = currentLerpTime / totalLerpTime;

                // Apply a slowdown factor when approaching the start position.
                if (t < slowdownDistance)
                {
                    t = Mathf.Pow(t / slowdownDistance, 2); // You can adjust this power to control the slowdown rate.
                }
                else if (t > 1.0f - slowdownDistance)
                {
                    t = 1.0f - Mathf.Pow((1.0f - t) / slowdownDistance, 2);
                }

                // Use Vector3.Lerp to move the object.
                transform.position = Vector3.Lerp(endPosition.position, startPosition.position, t);

                yield return null;
            }

            // Delay at the start position before repeating the loop.
            yield return new WaitForSeconds(loopDelay);
        }
    }
}
