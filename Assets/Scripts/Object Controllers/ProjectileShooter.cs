using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab you want to instantiate.
    public Transform spawnPoint; // The position where you want to spawn the projectiles.
    public float spawnInterval = 2f; // Time interval between each instantiation.
    public float projectileSpeed = 10f; // Force applied to the projectile.
    private void Start()
    {
        StartCoroutine(SpawnObjectCoroutine());
    }
    IEnumerator SpawnObjectCoroutine()
    {
        while (true)
        {
            // Instantiate the object
            GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
            // Set the velocity of the new projectile.
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            newProjectile.GetComponent<Animator>().SetTrigger("Expanse");
            Vector3 direction = spawnPoint.position - transform.position;
            rb.AddForce(direction * projectileSpeed);
            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
