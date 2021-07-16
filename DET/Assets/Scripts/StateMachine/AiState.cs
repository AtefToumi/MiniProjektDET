using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId
{
    ChasePlayer,
    AttackPlayer,
    Patrol
}
public interface AiState
{
    AiStateId GetId();
    void Enter(AiZombie zombie);
    void Update(AiZombie zombie);
    void Exit(AiZombie zombie);
}
