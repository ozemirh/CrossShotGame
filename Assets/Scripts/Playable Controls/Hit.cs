using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    Ground ground;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hit")
        {
            ground = Camera.main.GetComponent<CameraController>().GetGround();
            ground.BallHit();
        }
    }
}
