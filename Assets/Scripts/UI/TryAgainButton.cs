using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    StringBend stringBend;
    public Ground ground;
    public GameObject button;
    public GameObject foreGround;
    public bool clickable = true;
    CameraController controller;
    private void Start()
    {
        controller = Camera.main.GetComponent<CameraController>();
    }
    public void TryAgain()
    {
        if (clickable)
        {
            clickable = false;
            StartCoroutine(EnableClick());
            stringBend = controller.GetCrossbow().transform.GetChild(1).GetComponent<StringBend>();
            HasHealth();
        }
    }
    IEnumerator EnableClick()
    {
        yield return new WaitForSeconds(2);
        clickable = true;
    }
    public void HasHealth()
    {
        stringBend = Camera.main.GetComponent<CameraController>().GetCrossbow().transform.GetChild(1).GetComponent<StringBend>();
        stringBend.ResetVariables();
        ground.ReEnableEverything();
    }
}
