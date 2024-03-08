using System.Collections;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float speed = 10f;
    public float interval = 1f;
    public float duration;
    void Start()
    {
        // Start spawning objects at the specified interval
        StartCoroutine(SpawnObjectCoroutine());
    }

    IEnumerator SpawnObjectCoroutine()
    {
        while (true)
        {
            // Instantiate the object
            GameObject newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
            newProjectile.GetComponent<EliminateCannonball>().GetDuration(duration);
            // Set the velocity of the new projectile.
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            Vector2 direction = Quaternion.Euler(0, 0, launchPoint.rotation.eulerAngles.z) * Vector2.left;
            Vector2 velocity = direction * speed;
            rb.velocity = velocity;
            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(interval);
        }
    } 
}
