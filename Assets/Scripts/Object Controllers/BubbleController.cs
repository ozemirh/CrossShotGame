using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float elevateSpeed = 2f;
    public float triggerTime = 10f;
    private float elapsedTime = 0f;
    private bool triggered = false;
    public float growthSpeed = 0.1f;
    public float maxObjectSize = 5f;
    public ParticleSystem particle;
    private Rigidbody2D rb2d;
    private Animator animator;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Elevate the object using Rigidbody2D
        Elevate();

        // Check if it's time to trigger explosion
        if (!triggered && elapsedTime >= triggerTime)
        {
            Explosion();
            triggered = true;
        }

        // Increase the size of the object
        GrowObject();
    }

    void Elevate()
    {
        float verticalVelocity = elevateSpeed;
        rb2d.velocity = new Vector2(0, verticalVelocity);
        elapsedTime += Time.deltaTime;
    }

    void Explosion()
    {
        animator.SetTrigger("Explode");
    }

    void Explode()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void GrowObject()
    {
        if (transform.localScale.x < maxObjectSize)
        {
            float growthAmount = growthSpeed * Time.deltaTime;
            transform.localScale += new Vector3(growthAmount, growthAmount, growthAmount);
        }
        else if (transform.localScale.x >= maxObjectSize)
        {
            animator.enabled = true;
            animator.SetBool("Grew", true);
        }
    }
    public void ChangeAttributes(float elevate_Speed, float trigger_Time)
    {
        elevateSpeed = elevate_Speed;
        triggerTime = trigger_Time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hit")
        {
            Explosion();
        }
    }
}