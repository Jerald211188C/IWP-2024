using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] private RangeDetect _range;
    [SerializeField] private GameObject _player;

    private RoundController _roundController; 
    private LevelUpSystem _levelUpSystem;
    private Health health;
    public Health Health => health;
    private NavMeshAgent _agent;

    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;

    [Header("Speed Settings")]
    [SerializeField] private float minSpeed = 5f;  
    [SerializeField] private float maxSpeed = 10f;  

    private float _cooldown;
    private PlayerMovement _pm;

    private void Awake()
    {
        _levelUpSystem = FindFirstObjectByType<LevelUpSystem>();
        _roundController = FindFirstObjectByType<RoundController>();
     

        health = GetComponent<Health>();
        health.Death.AddListener(Death);

        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = Random.Range(minSpeed, maxSpeed);
        _player = GameObject.FindWithTag("Player");
        _pm = _player.GetComponent<PlayerMovement>();

        // Subscribe to player's death event if available
        Health playerHealth = _player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Death.AddListener(OnPlayerDestroyed);
        }
    }

    private void Update()
    {
        if (_player == null)
        {
            _agent.SetDestination(transform.position); // Stop moving
            return; // Exit Update
        }

        if (!_agent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent is not on the NavMesh.");
            return;
        }

        _agent.SetDestination(_player.transform.position); // Path towards the player

        _cooldown += Time.deltaTime; // Tick attack cooldown

        if (_range.InRange)
        {
            if (_cooldown > _attackCooldown && _pm != null)
            {
                _pm.Health.Damage(_damage); // Attack player
                _cooldown = 0; // Reset cooldown
            }
        }
    }

    private void OnPlayerDestroyed()
    {
        Debug.Log("Player destroyed. Clearing references.");
        _player = null; // Clear player reference
    }

    public void SetLevelSystem(LevelUpSystem _levelUpSystem)
    {
        this._levelUpSystem = _levelUpSystem;
    }
    private void Death()
    {
        if (_roundController != null)
        {
            _roundController._EnemyCount--;
        }

        _levelUpSystem.AddExp(_pm.EXP);
        _pm._Coins += _pm._CoinToAdd;

        Destroy(gameObject);
    }

   

}



