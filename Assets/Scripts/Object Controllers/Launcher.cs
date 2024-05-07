using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject rocket;
    private Vector3 targetPosition;
    private GameObject newRocket;
    public int chosenHoop = 0;
    public float moveSpeed = 5f;
    public float delayBeforeStart = 1f;
    public float accelerationTime = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SpawnRocket()
    {
        if (!newRocket)
        {
            newRocket = Instantiate(rocket, gameObject.transform);
            newRocket.GetComponent<Rocket>().SetRocketAttributes(mvSpeed: moveSpeed, dBS: delayBeforeStart, aTime: accelerationTime);
            StartCoroutine(MoveToTarget());
        }
    }
    IEnumerator MoveToTarget()
    {
        targetPosition = new Vector3(0, 0.5f, 0);
        if (newRocket != null )
        {
            Vector3 initialPosition = newRocket.transform.localPosition;
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                if (newRocket)
                {
                    float t = elapsedTime / 1f;
                    newRocket.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }
            // Ensure the object reaches exactly the target position at the end
            newRocket.transform.localPosition = targetPosition;
        }
    }
    public void ActivateRocket()
    {
        newRocket.GetComponent<Rocket>().enabled = true;
    }
    public int GetChosenHoop()
    {
        return chosenHoop;
    }
    public void CallEliminateRocket()
    {
        if (newRocket != null)
        {
            newRocket.GetComponent<Rocket>().EliminateRocket();
        }
    }
}
