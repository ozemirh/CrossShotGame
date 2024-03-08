using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 2f;  // Time between spawns in seconds
    public Transform spawnPos;
    private float timeSinceLastSpawn = 0f;
    public float elevateSpeed = 0f;
    public float triggerTime = 0f;
    public Vector3 push = new Vector3(0.0f, 1.0f, 0.0f);

    private void Update()
    {
        // Update timers
        timeSinceLastSpawn += Time.deltaTime;

        // Spawn object at regular intervals
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f;
        }
    }
    private void SpawnObject()
    {
        // Instantiate the object at the spawn position (you may want to change this based on your scene)
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPos.position, Quaternion.identity);
        // Access the Rigidbody component of the spawned object
        Rigidbody2D rigidbody = spawnedObject.GetComponent<Rigidbody2D>();
        // Check if the spawned object has a Rigidbody component
        if (rigidbody != null)
        {
            rigidbody.velocity = push;
        }
        spawnedObject.GetComponent<BubbleController>().ChangeAttributes(elevateSpeed, triggerTime);
    }
}
