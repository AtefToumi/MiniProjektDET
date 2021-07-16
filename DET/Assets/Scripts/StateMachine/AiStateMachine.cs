using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
    public AiState[] states;
    public AiZombie zombie;
    public AiStateId currentState;

    public AiStateMachine(AiZombie zombie)
    {
        this.zombie = zombie;
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        states = new AiState[numStates];
    }
    
    public void RegisterState(AiState state)
    {
        int index = (int)state.GetId();
        states[index] = state;    
    }

    public AiState GetState(AiStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    public void Update()
    {
        GetState(currentState)?.Update(zombie);
    }

    public void ChangeState(AiStateId newState)
    {
        GetState(currentState)?.Exit(zombie);
        currentState = newState;
        GetState(currentState)?.Enter(zombie);
    }
}
