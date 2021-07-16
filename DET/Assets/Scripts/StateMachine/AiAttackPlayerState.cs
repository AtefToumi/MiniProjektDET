using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    
    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }
    public void Enter(AiZombie zombie)
    {
        
    }
    public void Update(AiZombie zombie)
    {
        if (zombie.playerInSightRange && !zombie.playerInAttackRange)
        {
            zombie.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }

        zombie.anim.SetBool("InAttackRange", true);
        zombie.agent.SetDestination(zombie.transform.position);

        zombie.transform.LookAt(zombie.player);

        if (!zombie.alreadyAttacked)
        {
            //Instantiate the Grenade 
            zombie.InstGrenade();
            zombie.rb.AddForce(zombie.transform.up * 8f, ForceMode.Impulse);
            zombie.rb.AddForce(zombie.transform.forward * 32f, ForceMode.Impulse);

            zombie.alreadyAttacked = true;
            zombie.InvkAttack();    
        }
    }

    public void Exit(AiZombie zombie)
    {

    }
}
