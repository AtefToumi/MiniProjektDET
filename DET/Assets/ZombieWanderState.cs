using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWanderState : ZombieState
{
    public ZombieStateId GetId()
    {
        return ZombieStateId.Patrol;
    }
    public void Update(ZombieAi zombie)
    {
        if (zombie.playerInSightRange && !zombie.playerInAttackRange)
        {
            zombie.stateMachine.ChangeState(ZombieStateId.Chase);
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
    public void Enter(ZombieAi zombie)
    {

    }

    public void Exit(ZombieAi zombie)
    {

    }


}
