using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject startPos;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.Find("Chain Start");
        transform.position = startPos.transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartSwing();
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator HoldChain()
    {
        yield return new WaitForSeconds(0.3f);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void StartSwing()
    {
        StartCoroutine(HoldChain());
    }
    public void ResetChain()
    {
        transform.position = startPos.transform.position;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
