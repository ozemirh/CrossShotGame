using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public List<GameObject> hoops;
    public bool score;
    public const int hoopsNumber = 3;
    private CameraController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hoops.Count > 0) {
            if (controller.GetIsScored())
            {
                hoops[0].transform.GetChild(0).GetComponent<CrossbowSpawn>().SetOriginalBall(controller.GetBall());
                hoops[0].transform.GetChild(0).GetComponent<CrossbowSpawn>().SetOriginalCrossbow(controller.GetCrossbow());
                controller.SetScoreValue(false);
            }
        }
    }
    public void SetSpawnValues()
    {
        if (hoops.Count > 1)
        {
            hoops[1].SetActive(true);
        }
        hoops.Remove(hoops[0]);
    }
    public int GetHoopsCount()
    {
        return hoops.Count;
    }
    public GameObject GetActiveHoop()
    {
        int hoopIndex = hoopsNumber - hoops.Count;
        if (hoops.Count > 0)
        {
            return GameObject.Find("Hoop " + hoopIndex);
        }
        else
        {
            return null;
        }
    }
}
