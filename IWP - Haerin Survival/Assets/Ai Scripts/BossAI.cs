using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossAI : MonoBehaviour
{
    public enum BossState { Chase, Attack, Ultimate }
    public BossState currentState;

    [Header("References")]
    private NavMeshAgent agent;
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private GameObject _player;
    [SerializeField] Animator animator;
    [SerializeField] private RangeDetect _range;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClipAGain;

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;
    private bool Playonce;
    private bool Again;

    [Header("Attack Settings")]
    public float attackRange = 3f;

    [Header("Ultimate Ability Settings")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _damage;
    public float ultimateRange = 10f;
    public float ultimateCooldown = 10f;
    public float jumpForce = 15f;
    public float slamSpeed = 30f;
    public float knockbackForce = 10f;
    private bool isUltimateReady = true;
    private bool isJumping = false;
    private Vector3 targetSlamPosition;
    private PlayerMovement _pm;

    void Start()
    {
        Playonce = true;
        Again = false;
        _player = GameObject.FindWithTag("Player");
        _pm = _player.GetComponent<PlayerMovement>();

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        currentState = BossState.Chase;
        rb.isKinematic = true;
    }

    void Update()
    {
        switch (currentState)
        {
            case BossState.Chase:
                ChasePlayer();
                break;

            case BossState.Attack:
                AttackPlayer();
                break;

            case BossState.Ultimate:
                if (!isJumping)
                {
                    UltimateAbility();
                }
                break;
        }

        CheckStateTransitions();
    }

    void ChasePlayer()
    {
        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        agent.isStopped = false;
        agent.SetDestination(_player.transform.position);
    }

    void AttackPlayer()
    {
        if (agent.enabled)
        {
            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
        }
        Debug.Log("Boss is attacking!");
    }

    void UltimateAbility()
    {
        if (isUltimateReady)
        {
            Debug.Log("Boss is using Ultimate!");
            if (Playonce)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                Playonce = false;
                Again = true;
            }
            else if (Again)
            {
                audioSource.clip = audioClipAGain;
                audioSource.Play();
            }

            agent.isStopped = true;
            agent.enabled = false;
            rb.isKinematic = false;

            StartCoroutine(TrackPlayerBeforeSlam());

            rb.linearVelocity = Vector3.up * jumpForce;
            animator.SetBool("Jump", true);
            isJumping = true;
            isUltimateReady = false;
        }
    }

    IEnumerator TrackPlayerBeforeSlam()
    {
        float trackDuration = 1.0f;
        float timer = 0f;

        while (timer < trackDuration)
        {
            targetSlamPosition = _player.transform.position;
            timer += Time.deltaTime;
            yield return null;
        }

        SlamIntoGround();
    }

    void SlamIntoGround()
    {
        Debug.Log("Boss slamming into the ground!");
        Vector3 slamDirection = (targetSlamPosition - transform.position).normalized;
        rb.linearVelocity = slamDirection * slamSpeed;
        animator.SetBool("Jump", false);
        StartCoroutine(WaitForLanding());
    }

    private IEnumerator WaitForLanding()
    {
        while (!IsGrounded())
        {
            yield return null;
        }

        Debug.Log("Boss has landed!");
        isJumping = false;
        animator.SetTrigger("slam");
        if (_range.InRange)
        {
            _pm.Health.Damage(_damage);
        }
        ApplyKnockback();
        ReenableNavMesh();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 2f);
    }

    void ApplyKnockback()
    {
        if (_player != null)
        {
            PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Vector3 knockbackDirection = (_player.transform.position - transform.position).normalized;
                knockbackDirection.y = 0.5f;
                playerMovement.ApplyKnockback(knockbackDirection, knockbackForce);
            }
            else
            {
                Debug.LogWarning("No PlayerMovement script found on Player!");
            }
        }
    }

    void ReenableNavMesh()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        agent.enabled = true;
        agent.isStopped = false;
        agent.SetDestination(_player.transform.position);
        Invoke("ResetUltimate", ultimateCooldown);
    }

    void ResetUltimate()
    {
        isUltimateReady = true;
    }

    void CheckStateTransitions()
    {
        if (isJumping) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = BossState.Attack;
        }
        else if (distanceToPlayer <= ultimateRange && isUltimateReady)
        {
            currentState = BossState.Ultimate;
        }
        else
        {
            currentState = BossState.Chase;
            animator.SetBool("Jump", false);
        }
    }
}