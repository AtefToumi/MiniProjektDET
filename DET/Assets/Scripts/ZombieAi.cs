using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    public ZombieStateMachine stateMachine;
    public ZombieStateId initialState;
    public bool playerInSightRange, playerInAttackRange;
    public float sightRange, attackRange;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform playerTransform;
    public GameObject player;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public PlayerHealth playerHealth;
    public int zombieDamage;
    public bool isDead;
    public AiConfig aiConfig;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        aiConfig = GetComponent<AiConfig>();
        stateMachine = new ZombieStateMachine(this);
        stateMachine.ChangeState(initialState);
        stateMachine.RegisterState(new ZombieWanderState());
        stateMachine.RegisterState(new ZombieChaseState());
        stateMachine.RegisterState(new ZombieAttackState());
        playerHealth = player.GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        isDead = aiConfig.isDead; 
        stateMachine.Update();

    }

    public void InvkAttack()
    {
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    
}
