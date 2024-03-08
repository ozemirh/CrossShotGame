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
    CameraController controller;
    GameObject ball;
    public GameObject chainResetter;
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
        if (chainResetter != null && chainResetter.transform.parent.gameObject.activeSelf)
        {
            chainResetter.GetComponent<Chain>().StartSwing();
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
