using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    public bool shouldToggle;
    public bool shouldToggleLights;
    public bool shouldChangeColor;
    private bool isLightOn;
    Light2D light2D;
    public float toggleInterval;
    public float duration = 5.0f;  // Duration for one complete loop
    private float timer = 0.0f;    // Timer to track the progress of the loop
    public GameObject[] objects; // List of GameObjects to be activated
    public float activationDuration = 2f;
    public float deactivationDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(ToggleSwitch());
        StartCoroutine(ActivateObjects());
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldChangeColor)
        {
            ChangeColor();
        }
    }
    IEnumerator ActivateObjects()
    {
        if (shouldToggleLights)
        {
            while (true)
            {
                foreach (GameObject obj in objects)
                {
                    ActivateObject(obj);
                    yield return new WaitForSeconds(activationDuration);
                    DeactivateObject(obj);
                    yield return new WaitForSeconds(deactivationDuration);
                }
            }
        }
    }

    void ActivateObject(GameObject obj)
    {
        // Replace this with the actual code to turn on your object
        obj.SetActive(!obj.activeSelf);
    }

    void DeactivateObject(GameObject obj)
    {
        // Replace this with the actual code to turn off your object
        obj.SetActive(!obj.activeSelf);
    }
    IEnumerator ToggleSwitch()
    {
        while (shouldToggle)
        {
            isLightOn = !isLightOn;
            light2D.enabled = isLightOn;
            yield return new WaitForSeconds(toggleInterval);
        }
    }
    public void ChangeColor()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Calculate the normalized time within the loop (0 to 1)
        float normalizedTime = Mathf.Repeat(timer, duration) / duration;

        // Set HSV values based on the normalized time
        float hue = normalizedTime * 360f;
        float saturation = 1.0f;
        float value = 1.0f;

        // Convert HSV to RGB
        Color color = Color.HSVToRGB(hue / 360f, saturation, value);
        light2D.color = color;
    }
}
