using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine
{
    public ZombieState[] states;
    public ZombieAi zombie;
    public ZombieStateId currentState;

    public ZombieStateMachine(ZombieAi zombie)
    {
        this.zombie = zombie;
        int numStates = System.Enum.GetNames(typeof(ZombieStateId)).Length;
        states = new ZombieState[numStates];
    }

    public void RegisterState(ZombieState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public ZombieState GetState(ZombieStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    public void Update()
    {
        GetState(currentState)?.Update(zombie);
    }

    public void ChangeState(ZombieStateId newState)
    {
        GetState(currentState)?.Exit(zombie);
        currentState = newState;
        GetState(currentState)?.Enter(zombie);
    }
}
