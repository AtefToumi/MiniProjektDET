using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
     Transform player;
    ZombieHealth zombieHealth;
    private AudioSource zombiescreaming;

   NavMeshAgent nav;
   ZombieAttack zombieAttack;

   void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       nav = GetComponent<NavMeshAgent>();
       zombieHealth = GetComponent<ZombieHealth>();
       zombieAttack = GetComponent<ZombieAttack>();
        zombiescreaming = GetComponent<AudioSource>();
       
   }
  

    // Update is called once per frame
    void Update()
    {
        
        if (!zombieHealth.isDead )//&& !zombieAttack.playerInRange)
        {
              zombiescreaming.Play();
           // FindObjectOfType<AudioManager>().play("zombieScreaming");
            nav.SetDestination(player.position);
        }
        
      else
        {
            nav.enabled = false;
        } 
    } 
}
