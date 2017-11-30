using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {

    public AudioClip soundEffect;
	private Rigidbody2D rb;
    private string targetTag = "Enemy";
    private string wallTag = "Environment";
	public float speed;
    public int baseDamage = 1;

	// Use this for initialization
	void Start () {
        MusicAndSounds.instance.playSound(soundEffect);
		rb = GetComponent <Rigidbody2D> ();
		rb.velocity = transform.up * speed;
        Destroy(gameObject, 3f);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<EnemyScript>().hitPoints -= baseDamage;
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
