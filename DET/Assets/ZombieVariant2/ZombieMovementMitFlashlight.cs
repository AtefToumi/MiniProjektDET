using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovementMitFlashlight : MonoBehaviour
{
    Transform player;
    ZombieHealth zombieHealth;
    private AudioSource zombiescreaming;

    NavMeshAgent nav;
    ZombieAttack zombieAttack;

    Flashlight_PRO flashlightScript;
    GameObject flashlight;

    Animator anim;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        zombieHealth = GetComponent<ZombieHealth>();
        zombieAttack = GetComponent<ZombieAttack>();
        zombiescreaming = GetComponent<AudioSource>();
        flashlight = GameObject.FindGameObjectWithTag("flash");
        flashlightScript = flashlight.GetComponent<Flashlight_PRO>();
        anim = GetComponent<Animator>();
    }


   
    void Update()
    {

        if (!zombieHealth.isDead)//&& !zombieAttack.playerInRange)
        {
            zombiescreaming.Play();
        }


        if (!zombieHealth.isDead && flashlightScript.is_enabled )
        {
           anim.SetBool("lightON",true);
            nav.enabled = true;
            nav.SetDestination(player.position);
        }

        else
        {
            anim.SetBool("lightON",false);
            nav.enabled = false;

        }

    }
}

