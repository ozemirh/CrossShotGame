using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivater : MonoBehaviour
{
    public GameObject[] objectsToSwitch;  // Array of objects to switch
    public float activeDuration = 3f;     // Duration an object stays active (in seconds)
    public float inactiveInterval = 2f;   // Interval between switching objects (in seconds)

    private int currentIndex = 0;         // Index of the currently active object

    void Start()
    {
        // Make sure all objects are initially inactive
        foreach (GameObject obj in objectsToSwitch)
        {
            obj.SetActive(false);
        }

        // Start the switching process
        StartCoroutine(SwitchObjects());
    }

    IEnumerator SwitchObjects()
    {
        while (true)
        {
            // Activate the current object
            objectsToSwitch[currentIndex].SetActive(true);

            // Wait for the active duration
            yield return new WaitForSeconds(activeDuration);

            // Deactivate the current object
            objectsToSwitch[currentIndex].SetActive(false);

            // Move to the next object in the array
            currentIndex = (currentIndex + 1) % objectsToSwitch.Length;

            // Wait for the inactive interval before switching to the next object
            yield return new WaitForSeconds(inactiveInterval);
        }
    }
}
