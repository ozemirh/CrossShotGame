using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleExpanse : MonoBehaviour
{
    CrossbowSpawn crossbowSpawn;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CircleEnd()
    {
        crossbowSpawn.MoveObjects();
    }
    public void CircleSpawn(CrossbowSpawn crossbow)
    {
        crossbowSpawn = crossbow;
    }
    public void CloseCircleAnimation()
    {
        StartCoroutine(CloseCircle());
    }
    IEnumerator KillSpawn()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(transform.parent.gameObject);
    }
    IEnumerator CloseCircle()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetTrigger("CircleClose");
        StartCoroutine(KillSpawn());
    }
}
