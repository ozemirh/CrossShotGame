using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool moveOnXAxis = true;
    public float maxDistance = 10f; // Set your desired maximum distance
    private float totalDistance = 0f;
    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        float distanceToMove = speed * Time.deltaTime;

        if (moveOnXAxis)
        {
            // Check if moving in the positive X direction exceeds the maximum distance
            if (speed > 0 && totalDistance + distanceToMove > maxDistance)
            {
                distanceToMove = maxDistance - totalDistance;
                speed *= -1; // Reverse the direction
            }
            // Check if moving in the negative X direction exceeds the maximum distance
            else if (speed < 0 && totalDistance - distanceToMove < -maxDistance)
            {
                distanceToMove = totalDistance + maxDistance;
                speed *= -1; // Reverse the direction
            }

            transform.Translate(Vector3.right * distanceToMove);
            totalDistance += distanceToMove;
        }
        else
        {
            // Check if moving in the positive Y direction exceeds the maximum distance
            if (speed > 0 && totalDistance + distanceToMove > maxDistance)
            {
                distanceToMove = maxDistance - totalDistance;
                speed *= -1; // Reverse the direction
            }
            // Check if moving in the negative Y direction exceeds the maximum distance
            else if (speed < 0 && totalDistance - distanceToMove < -maxDistance)
            {
                distanceToMove = totalDistance + maxDistance;
                speed *= -1; // Reverse the direction
            }

            transform.Translate(Vector3.up * distanceToMove);
            totalDistance += distanceToMove;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed *= -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reverse the movement direction when triggered
        speed *= -1;
    }
}