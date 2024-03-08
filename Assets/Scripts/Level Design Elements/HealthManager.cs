using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject healthPrefab;
    public int health = 3;
    public HoopManager hoopManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReduceHealth()
    {
        if (hoopManager.GetHoopsCount() < 3)
        {
            health -= 1;
        }
    }
    public int GetHealthCount()
    {
        return health;
    }
}
