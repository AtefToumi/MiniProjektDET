using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    PlayerHealth playerHealth;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponentInChildren<LineRenderer>();
        gunLight = GetComponentInChildren<Light>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && playerHealth.playerDead)
        {
            Shoot();
        }

           if(timer >= timeBetweenBullets * effectsDisplayTime || playerHealth.playerDead)
           {
               DisableEffects();
           }
       }

       public void DisableEffects()
       {
           gunLine.enabled = false;
           gunLight.enabled = false;
       }  

        void Shoot()
        {
            if(!playerHealth.playerDead)
            {
                timer = 0f;
                gunLight.enabled = true;
                gunLine.enabled = true;
                gunLine.SetPosition(0, transform.position);
                shootRay.origin = transform.position;
                shootRay.direction = transform.forward;

                if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
                {
                    ZombieHealth zombieHealth = shootHit.collider.GetComponent<ZombieHealth>();
                    if (zombieHealth != null)
                    {
                        //  zombieHealth.TakeDamage(damagePerShot, shootHit.point);
                    }
                    gunLine.SetPosition(1, shootHit.point);
                }
                else
                {
                    gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
                }
            }
            else
            {
                playerHealth.playerDead = true;
            }

            
        }
    }

