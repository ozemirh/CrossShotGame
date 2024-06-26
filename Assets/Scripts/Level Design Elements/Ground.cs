using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Ground : MonoBehaviour
{
    StringBend stringBend;
    public GameObject foreGround;
    public GameObject button;
    public GameObject topRightButton;
    public HoopManager hoopManager;
    CameraController controller;
    GameObject ball;
    public GameObject chainResetter;
    public Launcher launchPad;
    GameObject activeHoop;
    private void Start()
    {
        controller = Camera.main.GetComponent<CameraController>();
        ball = controller.GetBall();
        stringBend = controller.GetCrossbow().transform.GetChild(1).GetComponent<StringBend>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallHit();
    }
    public void ReEnableEverything()
    {
        foreGround.SetActive(false);
        button.SetActive(false);
        ball.SetActive(true);
        StartCoroutine(BallWake());
        topRightButton.SetActive(true);
        if (launchPad != null && hoopManager != null)
        {
            SpawnRocketIfRight();
        }
        if (chainResetter != null && chainResetter.transform.parent.gameObject.activeSelf)
        {
            chainResetter.GetComponent<Chain>().StartSwing();
        }
    }
    public void SpawnRocketIfRight()
    {
        if (hoopManager != null)
        {
            activeHoop = hoopManager.GetActiveHoop();
            if (activeHoop == GetChosenHoop())
            {
                launchPad.SpawnRocket();
                launchPad.ActivateRocket();
            }
        }
    }
    public GameObject GetChosenHoop()
    {
        if (launchPad != null)
        {
            activeHoop = GameObject.Find("Hoop " + launchPad.GetChosenHoop());
            return activeHoop;
        }
        else
        {
            return null;
        }
    }
    public void EliminateRocketWhenScored()
    {
        if (activeHoop == GetChosenHoop())
        {
            launchPad.CallEliminateRocket();
        }
    }
    IEnumerator BallWake()
    {
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(.1f);
        stringBend.ChangePermission(true);
    }
    public void GetNewBall(GameObject gameObject)
    {
        ball = gameObject;
    }
    public void SetStringBend(StringBend bend)
    {
        stringBend = bend;
    }
    public void BallHit()
    {
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        ball.SetActive(false);
        topRightButton.SetActive(false);
        foreGround.SetActive(true);
        button.SetActive(true);
        stringBend.ChangePermission(false);
        if (chainResetter != null && chainResetter.transform.parent.gameObject.activeSelf)
        {
            chainResetter.GetComponent<Chain>().ResetChain();
        }
    }
}
