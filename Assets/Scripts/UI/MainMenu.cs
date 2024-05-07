using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartPlay()
    {
        SceneManager.LoadScene("Level " + (PlayerPrefs.GetInt("lastLevel", 1) - 1));
    }
    public void LevelsScreen()
    {
        SceneManager.LoadScene("Level Selection");
    }
}
