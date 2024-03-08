using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public float interval;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatFire());
    }
    void StartFire()
    {
        _particleSystem.Play();
        StartCoroutine(StopFire());
    }
    IEnumerator RepeatFire()
    {
        while (true)
        {
            StartFire();
            yield return new WaitForSeconds(interval);
        }
    }
    IEnumerator StopFire()
    {
        yield return new WaitForSeconds(duration);
        _particleSystem.Stop();
    }
}
