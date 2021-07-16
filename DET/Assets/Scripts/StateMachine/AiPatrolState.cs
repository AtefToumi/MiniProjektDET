using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }
    public void Update(AiZombie zombie)
    {
        if (zombie.playerInSightRange && !zombie.playerInAttackRange)
        {
            zombie.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }


        
        
            zombie.anim.SetBool("InAttackRange", false);
            zombie.anim.SetBool("InSightRange", false);

            if (!zombie.walkPointSet)
            {
                zombie.SearchWalkPoint();
            }

            if (zombie.walkPointSet)
            {
                zombie.agent.speed = 1f;
                zombie.agent.SetDestination(zombie.walkPoint);

            }

            Vector3 distanceToWalkPoint = zombie.transform.position - zombie.walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                zombie.walkPointSet = false;
        

        

    }
    public void Enter(AiZombie zombie)
    {

    }

    public void Exit(AiZombie zombie)
    {

    }


}
