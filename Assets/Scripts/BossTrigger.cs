using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{


    private BoxCollider2D bc;
    // Use this for initialization
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Environment.instance.setDoorsShut(true);
    }
}