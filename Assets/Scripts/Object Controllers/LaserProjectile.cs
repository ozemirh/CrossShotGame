using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public ParticleSystem effectPrefab;
    Ground ground;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Simulation Ball")
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }        
    }
}
