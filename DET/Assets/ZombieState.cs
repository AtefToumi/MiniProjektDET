using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieStateId
{
    Chase,
    Attack,
    Patrol
}
public interface ZombieState
{
    ZombieStateId GetId();
    void Enter(ZombieAi zombie);
    void Update(ZombieAi zombie);
    void Exit(ZombieAi zombie);
}
