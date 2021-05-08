using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public float sinkSpeed = 0.000001f;
    public int scoreValue = 10;

    Animator anim;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    public bool isDead;
    bool isSinking;

 

    void Awake()
    {
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
       
    }

    void Update()
    {
        if(isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime / 10);
        }
       
    }

    public void TakeDamage(int amount , Vector3 hitPoint)
    {
        if(isDead)
        return;
       // FindObjectOfType<AudioManager>().play("zombieDamage");
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        // hitParticles.transform.position = hitPoint;
        // hitParticles.Play();
        if (currentHealth <=0)
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
        Destroy(this.gameObject , 5f);
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 5f);
        ScoreManager.score += scoreValue;
    }
}
