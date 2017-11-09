using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDelete : MonoBehaviour {

    public float timer = 1.0f;
    public LayerMask collision;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
      /*  if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        */
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
	}
}
