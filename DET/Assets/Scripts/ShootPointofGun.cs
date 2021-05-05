using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointofGun : MonoBehaviour
{
    public int damage = 30;
    public float range = 100f;
    public ParticleSystem muzzelFlash;
    GameObject player;
    PlayerHealth playerHealth;

    private AudioSource shootingAudio;
    
    

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingAudio = GetComponent<AudioSource>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    // Update is called once per frame
    void Update()
    {
        
        
        
        if(Input.GetButtonDown("Fire1") && playerHealth.currentHealth > 0 )
        {
            Shoot();
        }
        
    }


    
    void Shoot()
    {

       
            muzzelFlash.Play();
            shootingAudio.Play();
       
            

            RaycastHit hitInfo;
            bool ishitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, range);

            if (ishitted)
            {
                
                Debug.Log(hitInfo.collider.name);
            
                ZombieHealth zombiehealth = hitInfo.transform.GetComponent<ZombieHealth>();
                if ( zombiehealth != null)
                {
                    zombiehealth.TakeDamage(damage);
                }
                Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
            }
        
    }
}
