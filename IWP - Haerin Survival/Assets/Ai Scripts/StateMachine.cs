using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    private HashSet<State> _states = new();
    private State _CurrentState;
    private State _NextState;

    public void RegisterState(State state)
    {
        _states.Add(state);
    }

    public void ChangeState(State state)
    {
        _NextState = state;
    }

    private void Update()
    {
        if (_NextState != null)
        {
            _CurrentState.Exit();
            _NextState.Enter();
            _CurrentState = _NextState;
            _NextState = null;
        }
        _CurrentState.Execute();
    }
}
