using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiZombie zombie)
    {

    }

    public void Exit(AiZombie zombie)
    {
        
    }

    public void Update(AiZombie zombie)
    {

        
        
        if (zombie.playerInSightRange && !zombie.playerInAttackRange)
        {
            zombie.anim.SetBool("InSightRange", true);
            zombie.anim.SetBool("InAttackRange", false);

            zombie.agent.speed = 3.5f;
            zombie.agent.SetDestination(zombie.player.position);
        }

        if (zombie.playerInAttackRange && zombie.playerInSightRange)
        {
            zombie.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }

    }

}
