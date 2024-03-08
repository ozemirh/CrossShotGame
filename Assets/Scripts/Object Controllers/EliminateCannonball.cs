using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateCannonball : MonoBehaviour
{
    float duration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetDuration(float newDuration)
    {
        duration = newDuration;
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
