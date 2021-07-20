using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieState
{
    public ZombieStateId GetId()
    {
        return ZombieStateId.Attack;
    }
    public void Enter(ZombieAi zombie)
    {
     
    }

    public void Exit(ZombieAi zombie)
    {
        
    }


    public void Update(ZombieAi zombie)
    {
        if (zombie.playerInSightRange && !zombie.playerInAttackRange)
        {
            zombie.stateMachine.ChangeState(ZombieStateId.Chase);
        }
        if (!zombie.alreadyAttacked && zombie.playerHealth.currentHealth > 0 && zombie.aiConfig.health > 0)
        {
            zombie.anim.SetBool("InAttackRange", true);
            zombie.playerHealth.TakeDamage(zombie.zombieDamage);
            zombie.alreadyAttacked = true;
            zombie.InvkAttack();
        }
    }
}
