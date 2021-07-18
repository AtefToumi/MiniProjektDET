
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;


    public float health;
    public Slider healthSlider;

    //Player Score
    public int scoreValue = 10;

    //Zombie Expliosion
    public GameObject orginalObject;
    public GameObject FracturedObject;

    private void Awake()
    {
       // player = GameObject.FindGameObjectWithTag("Player").transform ;
        //agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)   //TO-DO : wird noch nicht woanders aufgerufen 
    {
        
        health -= damage;
        healthSlider.value = health;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    private void DestroyEnemy()
    {
        ScoreManager.score += scoreValue;
        // anim.SetTrigger("Dead");
        // Destroy(gameObject,6f);
        Destroy(this.gameObject);
        GameObject fractobject = Instantiate(FracturedObject, transform.position, transform.rotation); // as GameObject;
        fractobject.GetComponent<SmallExplosion>().Explosion();
    }

  /*  private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    } */
}
