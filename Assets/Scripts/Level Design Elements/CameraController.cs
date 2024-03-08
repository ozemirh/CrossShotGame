using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ballPrefab;        
    private GameObject ball;
    public float offset = 4.2f;           
    private Vector3 ballPos;
    public HoopManager hoopManager;
    private GameObject crossbow;
    public bool isScored = false;
    public GameObject crossbowPrefab;
    public GameObject initialSpawnPos;
    private GameObject initialCrossbow;
    private GameObject initialBall;
    public Color32 crossbowColor;
    public Color32 ballColor;
    public float cameraAscendSpeed = 1.0f;
    public Ground ground;
    public float launchForce = 10f;
    // Use this for initialization
    void Start()
    {
        
    }
    private void Awake()
    {
        crossbow = Instantiate(crossbowPrefab, initialSpawnPos.transform.position, Quaternion.identity);
        crossbow.transform.GetChild(1).GetComponent<StringBend>().GetCamera(gameObject.GetComponent<Camera>());
        crossbow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = crossbowColor;
        crossbow.transform.GetChild(1).GetComponent<StringBend>().ChangeLaunchForce(launchForce);
        ball = Instantiate(ballPrefab, initialSpawnPos.transform.position, Quaternion.identity);
        ball.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ballColor;
        ball.transform.GetComponent<BallAscend>().enabled = false;
        crossbow.transform.GetChild(1).GetComponent<StringBend>().StartPermissionCoroutine();
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        ballPos = new Vector3(0, ball.transform.position.y, -10);
        Vector3 offsetVector = new Vector3(0, offset);
        if (ballPos.y + offsetVector.y >= -0.44f)
        transform.position = ballPos + offsetVector;
    }
    public void SetScoreValue(bool score)
    {
        isScored = score;
    }
    public bool GetIsScored()
    {
        return isScored;
    }
    public GameObject GetCrossbow()
    {
        return crossbow;
    }
    public GameObject GetBall()
    {
        return ball;
    }
    public Ground GetGround()
    {
        return ground;
    }
    public float GetCameraSpeed()
    {
        return cameraAscendSpeed;
    }
    public void CreateBall(Vector3 spawnPos, Quaternion spawnRot)
    {
        ball = Instantiate(ballPrefab, spawnPos, spawnRot);
        ball.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ballColor;
    }
    public void CreateCrossbow(Vector3 spawnPos, Quaternion spawnRot, float force)
    {
        crossbow = Instantiate(crossbowPrefab, spawnPos, spawnRot);
        crossbow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = crossbowColor;
        crossbow.transform.GetChild(1).GetComponent<StringBend>().ChangeLaunchForce(force);    
        if (hoopManager.GetActiveHoop() != null)
        {
            cameraAscendSpeed = hoopManager.GetActiveHoop().transform.GetChild(0).GetComponent<CrossbowSpawn>().GetActiveAscendSpeed();
        }
    }
    public void ShrinkCameraAnimation()
    {
        StartCoroutine(AnimateCameraSize(Camera.main.orthographicSize, 5.2f, 0.5f));
    }
    IEnumerator AnimateCameraSize(float startSize, float endSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, endSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.orthographicSize = endSize;
    }
}
