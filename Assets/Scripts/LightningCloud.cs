using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LightningCloud : AbilitySystem
{
    private Animator anim;
    public AudioClip soundEffect;
    private EnemyScript enemy;
    private Shock stun;
    private const string aName = "Lightning Cloud";
    private const string aDescription = "A lightning spell stuns an object on impact";
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private string targetTag = "Player";

    // ability parameter 
    public const float shockDuration = 1f;
    public const float baseDamage = 8f;
    public float speed = 20f;

    private Stopwatch durationTimer = new Stopwatch();

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        Invoke("Strike", 2f);
    }

    void Strike()
    {
        UnityEngine.Debug.Log("Test");
        bc.enabled = true;
        anim.enabled = true;
        anim.Play("LightningCloud", 0, 0f);
        MusicAndSounds.instance.playSound(soundEffect);
        Destroy(gameObject, 1.2f);
    }


    public LightningCloud() : base(new BasicObjectInfo(aName, aDescription))
    {
        this.AbilityBehaviors.Add(new Shock(2f, shockDuration, baseDamage));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Collision");
        if (other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<PlayerMobility>().currentHealth -= baseDamage;
            other.gameObject.GetComponent<PlayerMobility>().adjustHealth();
            other.gameObject.GetComponent<PlayerMobility>().speed = 0f;
            other.gameObject.GetComponent<PlayerShooting>().setFireRate(0f);

            StartCoroutine(STUN(other.gameObject));
        }

    }

    private IEnumerator STUN(GameObject objectHit)
    {

        yield return new WaitForSeconds(shockDuration);
        objectHit.GetComponent<PlayerMobility>().speed = 5f;
        objectHit.gameObject.GetComponent<PlayerShooting>().setFireRate(0.5f);
        Destroy(gameObject);

        yield return null;

    }
}

