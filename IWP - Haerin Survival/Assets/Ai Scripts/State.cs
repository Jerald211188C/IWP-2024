using UnityEngine;

public abstract class State : MonoBehaviour
{
    private StateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine= GetComponent<StateMachine>();
        _stateMachine.RegisterState(this);

    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
