﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    float cooldownTimer = 0;
    public float fireDelay = 0.1f;
    private float skillCost1 = 2.0f;
    private float skillCost2 = 6.0f;
    private float skillCost3 = 10.0f;
    private float chargeCost = 0f;

    private int skillSelection;
    public Image SkillIcon;
    public Sprite[] SkillIconList;

    public Transform shotSpawn;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public GameObject fireball;
    public GameObject lightning;

    private float fireRate = 0.5f;
    private float nextFire;

    // Use this for initialization
    void Start()
    {
        skillSelection = 1;
        SkillIcon.sprite = SkillIconList[skillSelection - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!Environment.instance.isDoingSetup())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                skillSelection = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Environment.instance.getSkillUnlocked(0))
            {
                skillSelection = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && Environment.instance.getSkillUnlocked(1))
            {
                skillSelection = 3;
            }

            SkillIcon.sprite = SkillIconList[skillSelection - 1];

            cooldownTimer -= Time.deltaTime;
            /* Vector3 shootDir;
            shootDir = Input.mousePosition;
            shootDir.z = 0.0f;
            shootDir = Camera.main.ScreenToWorldPoint(shootDir);
            shootDir = shootDir - transform.position;
            */
            var pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Vector2 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotSpawn.position;
            /*float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            shotSpawn.rotation = Quaternion.Slerp(shotSpawn.rotation, rotation, 5f * Time.deltaTime);*/
            shotSpawn.up = mouseDir;
            shotSpawn2.up = Quaternion.Euler(0, 0, -10) * mouseDir;
            shotSpawn3.up = Quaternion.Euler(0, 0, 10) * mouseDir;

            var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
            Vector3 offsetPosition = q * new Vector3(0.5f, 0f, 0f);
            //Debug.Log("Offset: " + offsetPosition);

            offsetPosition = transform.position;
            //Debug.Log("Player: " + transform.position);
            //Debug.Log("Shot: " + offsetPosition);

            if (Input.GetMouseButton(0) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 1)
            {
                cooldownTimer = fireDelay;
                Instantiate(bulletPrefab, offsetPosition, shotSpawn.rotation);
                //Debug.Log("FIRE");
                Environment.instance.setCurrentMpAfterSkill(skillCost1);
            }

            if (Input.GetMouseButton(0) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 2)
            {
                cooldownTimer = fireDelay;
                offsetPosition = transform.position;
                Instantiate(fireball, offsetPosition, shotSpawn.rotation);
                Environment.instance.setCurrentMpAfterSkill(skillCost2);
            }

            if (Input.GetMouseButton(1) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 2)
            {
                cooldownTimer = fireDelay;
                Vector3 oldTransform = fireball.transform.localScale;
                if (fireball.transform.localScale.x < 9)
                {
                    fireball.transform.localScale += new Vector3(2, -2, 0);
                    chargeCost += (skillCost2 / 2f);
                    fireball.GetComponent<FireBall>().baseDamage += 0.5f;
                }
                else
                {
                    Instantiate(fireball, offsetPosition, q);
                    fireball.transform.localScale = new Vector3(1, -1, 1);
                    fireball.GetComponent<FireBall>().baseDamage = 1f;
                    Environment.instance.setCurrentMpAfterSkill(skillCost2 + chargeCost);
                    chargeCost = 0f;
                }
            }
            else if (Input.GetMouseButtonUp(1) && skillSelection == 2 && Environment.instance.getmanaChargeState() == false)
            {
                Instantiate(fireball, offsetPosition, q);
                fireball.transform.localScale = new Vector3(1, -1, 1);
                fireball.GetComponent<FireBall>().baseDamage = 1f;
                Environment.instance.setCurrentMpAfterSkill(skillCost2 + chargeCost);
                chargeCost = 0f;
            }

            if (Input.GetMouseButton(0) && cooldownTimer <= 0 && Environment.instance.getWhichSkill() == 1 && Environment.instance.getmanaChargeState() == false && skillSelection == 3)
            {
                cooldownTimer = fireDelay;
                Instantiate(lightning, offsetPosition, shotSpawn.rotation);
                Instantiate(lightning, offsetPosition, shotSpawn2.rotation);
                Instantiate(lightning, offsetPosition, shotSpawn3.rotation);
                Environment.instance.setCurrentMpAfterSkill(skillCost3);
            }

            if (Environment.instance.getLevelUpReady(2))
            {
                //improve Arcane Bolt
                Environment.instance.setLevelUpReady(false, 2);
            }

            if (Environment.instance.getLevelUpReady(3))
            {
                //unlock and improve Fireball
                Environment.instance.setLevelUpReady(false, 3);
            }

            if (Environment.instance.getLevelUpReady(4))
            {
                //unlock and improve Lightning Bolt
                Environment.instance.setLevelUpReady(false, 4);
            }
        }
    }

    public void setFireRate(float theRate)
    {
        fireRate = theRate;
    }

    /*void FixedUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        Vector3 relativePos = mousePosition - shotSpawn.position;

        shotSpawn.localPosition = new Vector3(Mathf.Clamp(relativePos.x, 0, 0), Mathf.Clamp(relativePos.y, 0, 0), 0);
        shotSpawn.rotation = rot;
        shotSpawn.eulerAngles = new Vector3(0, 0, shotSpawn.eulerAngles.z);

        shotSpawn2.localPosition = new Vector3(Mathf.Clamp(relativePos.x, 0, 0), Mathf.Clamp(relativePos.y, 0, 0), 0);
        shotSpawn2.rotation = rot;
        shotSpawn2.eulerAngles = new Vector3(0, 0, shotSpawn.eulerAngles.z + 10);

        shotSpawn3.localPosition = new Vector3(Mathf.Clamp(relativePos.x, 0, 0), Mathf.Clamp(relativePos.y, 0, 0), 0);
        shotSpawn3.rotation = rot;
        shotSpawn3.eulerAngles = new Vector3(0, 0, shotSpawn.eulerAngles.z - 10);
    }*/
    
}
