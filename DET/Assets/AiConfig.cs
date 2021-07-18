using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AiConfig : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool isDead;
    public int currentHealth;
    [HideInInspector]public Animator anim;
    public CapsuleCollider capsuleCollider;
    public bool isSinking;
    public float health;
    public Slider healthSlider;
    public PlayerHealth playerHealth;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void TakeDamage(int amount, Vector3 hitPoint)
    //{
    //    if (isDead)
    //        return;
    //    currentHealth -= amount;
    //    //healthSlider.value = currentHealth;
    //    if (currentHealth <= 0)
    //    {
    //        Death();
    //    }
    //}

    public void TakeDamage(int damage)   //TO-DO : wird noch nicht woanders aufgerufen 
    {

        health -= damage;
        //healthSlider.value = health;

        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        StartSinking();
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        Destroy(this.gameObject, 5f);
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy(this.gameObject, 5f);
    }
}
