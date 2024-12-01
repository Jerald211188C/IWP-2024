using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float _Health;
    [SerializeField] protected float _Damage;
    private StateMachine _StateMachine;

    private void Awake()
    {
        _StateMachine = GetComponent<StateMachine>();

    }
}
