using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    StringBend stringBend;
    public Ground ground;
    public void ResetEverything()
    {
        stringBend = Camera.main.GetComponent<CameraController>().GetCrossbow().transform.GetChild(1).GetComponent<StringBend>();
        stringBend.ResetVariables();
        ground.ReEnableEverything();
    }
}
