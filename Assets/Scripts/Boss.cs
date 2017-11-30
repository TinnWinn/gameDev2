using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyScript {

    public GameObject rightArm;
    public GameObject leftArm;
    private BoxCollider2D colliderRight;
    private BoxCollider2D colliderLeft;
    public GameObject LightningCloud;
    private Transform target;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Animator animRight, animLeft;
    private float cooldownTimer = 10f;
    private float cooldownTimerHit = 10f;
    private float meleeDelay = 15f;
    private float fireDelay = 15f;

	// Use this for initialization
	void Start () {
        animRight = rightArm.GetComponent<Animator>();
        animLeft = leftArm.GetComponent<Animator>();
        animRight.enabled = false;
        animLeft.enabled = false;
        colliderRight = rightArm.GetComponent<BoxCollider2D>();
        colliderLeft = leftArm.GetComponent<BoxCollider2D>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Environment.instance.isDoingSetup()) {
            if (cooldownTimer <= 0f && Environment.instance.getBossActive() && !Environment.instance.isDoingSetup())
            {
                cooldownTimer = fireDelay;
                animRight.enabled = true;
                colliderRight.offset = new Vector2(-0.95f, 0f);
                colliderRight.size = new Vector2(1.9f, 4f);
                animRight.Play("BossRightArm", 0, 0.0f);
                Instantiate(LightningCloud, target.position + Vector3.up, Quaternion.identity);
                Invoke("rightArmDown", 0.2f);
            }
            cooldownTimer -= Time.deltaTime;
            cooldownTimerHit -= Time.deltaTime;

            if (Vector3.Distance(target.position, transform.position) < 15f && cooldownTimerHit <= 0f)
            {
                cooldownTimerHit = meleeDelay;
                if (target.position.x > transform.position.x)
                {
                    animLeft.enabled = true;
                    colliderLeft.offset = new Vector2(1.3f, -0.5f);
                    colliderLeft.size = new Vector2(1.5f, 2.2f);
                    animLeft.Play("BossLeftArm", 0, 0f);
                    Invoke("leftArmDown", 0.2f);
                }
                else
                {
                    animRight.enabled = true;
                    colliderRight.offset = new Vector2(-0.95f, 0f);
                    colliderRight.size = new Vector2(1.9f, 4f);
                    animRight.Play("BossRightArm", 0, 0f);
                    Invoke("rightArmDown", 0.2f);
                    
                }
            }
        }
	}

    void leftArmDown()
    {
        animLeft.Play("BossLeftArmReverse", 0, 0.1f);
        colliderLeft.offset = new Vector2(0.95f, -0.5f);
        colliderLeft.size = new Vector2(0.7f, 2.2f);
    }

    void rightArmDown()
    {
        animRight.Play("BossRightArmReverse", 0, 0.1f);
        colliderRight.offset = new Vector2(-0.95f, -0.66f);
        colliderRight.size = new Vector2(0.7f, 2.5f);
    }
}
