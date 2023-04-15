using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private GameState _currentState;
    private readonly List<GameState> _states;

    public GameStateMachine(List<GameState> states)
    {
        _states = states;
        _currentState = FindState<BuildState>();
        _currentState.Enter();
    }

    public void TrySwitchState<T>(){
        GameState nextState = FindState<T>();
        if (_currentState.CanTransit(nextState))
        {
            SwitchState(nextState);
            Debug.Log(nextState);
        }

    }

    private void SwitchState(GameState state)
    {
        _currentState.Transit(state);
        _currentState = state;
        _currentState.Enter();
    }

    private GameState FindState<T>() 
        => _states.Find(x => x is T) ?? throw new InvalidOperationException();
}