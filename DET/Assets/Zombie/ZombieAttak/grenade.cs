using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
  //  public GameObject ExplosionEffect;  //TO-DO : diese Effect erstellen 

    PlayerHealth playerHealth;
    GameObject player;

    public int grenadeDamage = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    public void OnCollisionEnter(Collision other)
    {
     //   Instantiate(ExplosionEffect, transform.position, transform.rotation);
        
        if (other.gameObject.tag == "Player")
        {
            
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(grenadeDamage);
            }
        }


        Destroy(this.gameObject);
    }

} 