using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject objectToFollow;
    public float moveSpeed = 5f; // Adjust this speed as needed
    public float delayBeforeStart = 2f; // Delay before the object starts moving
    public float accelerationTime = 2f; // Time to reach maximum speed
    private CameraController cameraController;
    public GameObject explosion;
    private float startTime;
    private float currentValue = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        cameraController = Camera.main.GetComponent<CameraController>();
        objectToFollow = cameraController.GetBall();
    }
    public void SetRocketAttributes(float mvSpeed, float dBS, float aTime)
    {
        moveSpeed = mvSpeed;
        delayBeforeStart = dBS;
        accelerationTime = aTime;
    }
    private void FixedUpdate()
    {
        if (objectToFollow != null && objectToFollow.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            // Update the current value based on the speed
            currentValue += Time.deltaTime;
            // Check if the current value has reached or exceeded the target value
            if (currentValue >= delayBeforeStart)
            {
                StartCoroutine(EliminationCountdown());
                rb.bodyType = RigidbodyType2D.Dynamic;
                // Calculate direction to the object to follow
                Vector2 direction = (objectToFollow.transform.position - transform.position).normalized;
                float timeSinceStart = Time.time - (startTime + delayBeforeStart); // Calculate the time since the object started moving
                float currentSpeed = Mathf.Lerp(1f, moveSpeed, timeSinceStart / accelerationTime); // Interpolate between 1 and moveSpeed
                // Move the object towards the object to follow with given speed using Rigidbody2D forces
                rb.velocity = direction * currentSpeed;
                Vector2 lookDirection = objectToFollow.transform.position - transform.position;
                float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) - 90;
                rb.rotation = angle;
            }
        }
        else
        {
            objectToFollow = cameraController.GetBall();
            startTime = Time.time;
        }
    }
    IEnumerator EliminationCountdown()
    {
        yield return new WaitForSeconds(5f);
        EliminateRocket();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EliminateRocket();
    } 
    public void EliminateRocket()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
