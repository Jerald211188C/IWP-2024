using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] private RangeDetect _range;
    [SerializeField] private GameObject _player;
    private RoundController _roundController; // Reference to the RoundController
    private Health health;
    public Health Health => health;
    private NavMeshAgent _agent;

    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;

    [Header("Speed Settings")]
    [SerializeField] private float minSpeed = 5f;  // Minimum speed for the enemy
    [SerializeField] private float maxSpeed = 10f;  // Maximum speed for the enemy

    private float _cooldown;
    private PlayerMovement _pm;

    private void Awake()
    {
        // Find the RoundController in the scene
        //_roundController = FindObjectOfType<RoundController>();
        _roundController = FindFirstObjectByType<RoundController>();
        if (_roundController == null)
        {
            Debug.LogError("RoundController not found in the scene. Make sure it exists.");
        }

        health = GetComponent<Health>();
        health.Death.AddListener(Death);

        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = Random.Range(minSpeed, maxSpeed);
        // Find the player if not assigned
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player");
            if (_player == null)
            {
                Debug.LogWarning("Player reference is not assigned, and no GameObject with the 'Player' tag was found.");
                return;
            }
        }

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

    private void Death()
    {
        if (_roundController != null)
        {
            _roundController._EnemyCount--; // Decrement the enemy count in RoundController
        }

        // Additional death logic
        Destroy(gameObject);
        Debug.Log("Enemy has died.");
    }
}
