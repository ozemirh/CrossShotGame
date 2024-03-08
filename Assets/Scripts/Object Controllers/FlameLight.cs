using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class FlameLight : MonoBehaviour
{
    public float lightOffset = 0.5f; // Offset between particle and light

    private List<GameObject> lights = new List<GameObject>(); // Keep track of created lights
    ParticleSystem particle;

    void Start()
    {
    }

    void Assign2DLight(GameObject particle)
    {
        if (particle!= null)
        {
            // Create a 2D Light
            GameObject lightGameObject = new GameObject("2D Light");
            lightGameObject.transform.position = particle.transform.position;

            // Attach a 2D Light component to the new GameObject
            Light2D light2D = lightGameObject.AddComponent<Light2D>();

            // Customize the light properties as needed
            light2D.color = Color.white;
            light2D.intensity = 1.0f;
            light2D.pointLightInnerRadius = 0.1f;
            light2D.pointLightOuterRadius = 1.0f;

            // Parent the light to the particle
            lightGameObject.transform.parent = particle.transform;

            // Add the light to the list for tracking
            lights.Add(lightGameObject);
        }
        else
        {
            Debug.LogWarning("Particle object does not have a Particle System component.");
        }
    }

    void OnDestroy()
    {
        // Clean up lights when the script's GameObject is destroyed
        foreach (GameObject light in lights)
        {
            Destroy(light);
        }
    }
}
