using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiZombie : MonoBehaviour
{
    [HideInInspector]public Rigidbody rb;
    [HideInInspector]public Animator anim;
    [HideInInspector]public Transform player;

    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public UnityEngine.AI.NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject grenade;
    public GameObject rightHand;
    public bool alreadyAttacked;
    public float timeBetweenAttacks;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;



    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new AiStateMachine(this);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.RegisterState(new AiPatrolState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        stateMachine.Update(); 
    }

    public void InstGrenade()
    {
        rb = Instantiate(grenade, rightHand.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
    }

    public void InvkAttack()
    {
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
    private void ResetAttack()
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
