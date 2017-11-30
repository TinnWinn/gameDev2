using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public AudioClip doorSound;

    private bool open = false;
    private GameObject theDoor;
    private BoxCollider2D boxCollider;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        boxCollider = GetComponent<BoxCollider2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!open)
        {
            if (collision.collider.gameObject.name == "Player" || collision.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Door");
                animator.enabled = true;
                animator.Play("DoorAnim", 0, 0f);
                MusicAndSounds.instance.playSound(doorSound);
                Invoke("letThrough", 0.5f);
            }
        }
            
    }

    private void letThrough()
    {
        gameObject.layer = 0;
        open = true;
    }

    private void closeDoor()
    {
        gameObject.layer = 8;
        animator.SetTrigger("DoorRev");
        MusicAndSounds.instance.playSound(doorSound);
        open = false;
    }

    // Update is called once per frame
    void Update () {
		if(open)
        {
            Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            GameObject[] enemyPos = GameObject.FindGameObjectsWithTag("Enemy");
            float xDir = playerPos.position.x - transform.position.x;
            float yDir = playerPos.position.y - transform.position.y;
            float dist = Mathf.Sqrt(Mathf.Pow(xDir, 2) + Mathf.Pow(yDir, 2));
            if (dist <= 2f)
            {
                return;
            }
            for(int i = 0; i < enemyPos.Length; i++)
            {
                xDir = enemyPos[i].transform.position.x - transform.position.x;
                yDir = enemyPos[i].transform.position.y - transform.position.y;
                dist = Mathf.Sqrt(Mathf.Pow(xDir, 2) + Mathf.Pow(yDir, 2));
                if (dist <= 2f)
                    return;
            }
            closeDoor();
        }
	}
}
