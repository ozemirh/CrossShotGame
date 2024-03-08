using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject foreGround;
    public GameObject button;
    public TMP_Text text;
    public GameObject topRightButton;
    CameraController controller;
    StringBend stringBend;
    // Start is called before the first frame update
    void Start()
    {
        controller = Camera.main.GetComponent<CameraController>();
        stringBend = controller.GetCrossbow().transform.GetChild(1).GetComponent<StringBend>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelEndUI()
    {
        foreGround.SetActive(true);
        foreGround.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 129);
        button.SetActive(true);
        text.gameObject.SetActive(true);
        text.text = (SceneManager.GetActiveScene().name + " is completed");
        stringBend.ChangePermission(false);
        topRightButton.SetActive(false);
    }
}
