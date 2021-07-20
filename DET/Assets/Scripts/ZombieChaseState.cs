using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseState : ZombieState
{
    
    public ZombieStateId GetId()
    {
        return ZombieStateId.Chase;
    }
    public void Enter(ZombieAi zombie)
    {
        
    }

    public void Exit(ZombieAi zombie)
    {
        
    }


    public void Update(ZombieAi zombie)
    {
        if (zombie.playerInSightRange && zombie.playerInAttackRange && zombie.aiConfig.health > 0)
        {
            zombie.stateMachine.ChangeState(ZombieStateId.Attack);
        }

        else if (zombie.playerInAttackRange && zombie.playerInSightRange && zombie.aiConfig.health > 0)
        {
            zombie.stateMachine.ChangeState(ZombieStateId.Attack);
        }

        else
        {
            if (zombie.aiConfig.health > 0)
            {
                zombie.anim.SetBool("InSightRange", true);
                zombie.anim.SetBool("InAttackRange", false);

                zombie.agent.speed = 3.5f;
                zombie.agent.SetDestination(zombie.playerTransform.position);
            }
            
        }

    }
}
