
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public Slider healthSlider;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject grenade;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animator
    Animator anim;
    public GameObject rightHand;

    //Player Score
    public int scoreValue = 10;

    //Zombie Expliosion
    public GameObject orginalObject;
    public GameObject FracturedObject;

    private void Awake()
    {
       // player = GameObject.FindGameObjectWithTag("Player").transform ;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            anim.SetBool("InAttackRange", false);
            anim.SetBool("InSightRange", false);

            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            anim.SetBool("InSightRange", true);
            anim.SetBool("InAttackRange", false);

            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
        
            anim.SetBool("InAttackRange", true);
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
         SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.speed = 1f;
            agent.SetDestination(walkPoint);

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;     
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = 3.5f;
        agent.SetDestination(player.position);
    }


    // Attack
    private void AttackPlayer()
    {
        //enemy cann't move during Attack

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Instantiate the Grenade 
            Rigidbody rb = Instantiate(grenade, rightHand.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
           
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
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
