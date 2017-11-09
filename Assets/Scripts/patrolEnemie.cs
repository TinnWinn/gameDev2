using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolEnemie : MonoBehaviour {

    public float speed1;
    public Transform target1;
    public GameObject shot1;
    public Transform shotSpawn1;
    public float fireRate1;
    public float chaseRange;
    public Transform[] patrolPoints;

    private float nextFireagain;
    private Rigidbody2D rb1;

    Transform currentPatrolPoint;
    int currentPatrolIndex;

    // Use this for initialization
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    void Update()
    {
        if (!Environment.instance.isDoingSetup())
        {
            //Chase Player AI
            float distanceToTarget = Vector3.Distance(transform.position, target1.position);
            if (distanceToTarget < chaseRange)
            {
                if (Time.time > nextFireagain)
                {
                    nextFireagain = Time.time + fireRate1;
                    Instantiate(shot1, shotSpawn1.position, shotSpawn1.rotation);
                }

                Vector3 targetDirection = target1.position - transform.position;
                float chaseAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion q2 = Quaternion.AngleAxis(chaseAngle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q2, 180);

                transform.Translate(Vector3.up * Time.deltaTime * speed1);
            }
            else
            {

                //Enemy Patrolling AI
                transform.Translate(Vector3.up * Time.deltaTime * speed1);
                if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
                {
                    if (currentPatrolIndex + 1 < patrolPoints.Length)
                    {
                        currentPatrolIndex++;
                    }
                    else
                    {
                        currentPatrolIndex = 0;
                    }
                    currentPatrolPoint = patrolPoints[currentPatrolIndex];
                }
                Vector3 patrolPointDirection = currentPatrolPoint.position - transform.position;
                float angle = Mathf.Atan2(patrolPointDirection.y, patrolPointDirection.x) * Mathf.Rad2Deg - 90f;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
            }
        }

        /*
	void FixedUpdate () {
		float z = Mathf.Atan2 ((target.transform.position.y - transform.position.y), 
			          (target.transform.position.x - transform.position.x)) *
		          Mathf.Rad2Deg - 90;
		transform.eulerAngles = new Vector3 (0, 0, z);
		rb.AddForce (gameObject.transform.up * speed);
	}
	*/
    }
}
