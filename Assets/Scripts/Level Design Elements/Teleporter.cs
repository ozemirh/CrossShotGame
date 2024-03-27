using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform exit;

    void Start()
    {
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a specific tag (you can customize this)
        if (other.tag != "Walls" && other.tag != "Obstacle")
        {
            if (other.tag == "Ball")
            {
                other.transform.parent.transform.position = exit.position;
            }
            else
            {
                other.transform.position = exit.position;
            }
        }
    }
}
