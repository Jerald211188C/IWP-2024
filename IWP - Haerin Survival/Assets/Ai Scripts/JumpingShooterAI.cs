using UnityEngine;
using UnityEngine.AI;

public class JumpingShooterAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;  // Player reference
    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool isComboActive = false;
    private GameObject _player;
    private PlayerMovement _pm;

    [Header("AI Settings")]
    public float detectionRange = 3f; // Range at which AI starts combo
    public float comboDuration = 0.5f; // Time between each combo hit
    public float comboHeight = 3f; // Upward force per hit
    public float comboSpeed = 5f; // Upward speed multiplier
    public float finalSlamForce = 10f; // Force applied to slam the player down

    private int comboCount = 0;
    private float comboTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
        _pm = _player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isComboActive)
        {
            // Move toward the player
            agent.SetDestination(_player.transform.position);

            // Check if AI is close enough to attack
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                StartCombo();
            }
        }
        else
        {
            comboTimer += Time.deltaTime;

            // Apply combo hits
            if (comboTimer >= comboDuration && comboCount < 3)
            {
                ApplyComboHit();
            }

            // After 3 hits, slam the player
            if (comboCount >= 3 && comboTimer >= comboDuration)
            {
                SlamPlayer();
            }
        }
    }

    void StartCombo()
    {
        isComboActive = true;
        comboTimer = 0f;
        comboCount = 0;

        // Stop AI movement during the combo
        agent.isStopped = true;
    }

    void ApplyComboHit()
    {
        if (comboCount < 3)
        {
            comboCount++;

            // Push the player upwards
            Vector3 upwardForce = Vector3.up * comboHeight;
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb)
            {
                playerRb.linearVelocity = upwardForce * comboSpeed;
            }

            // Reset timer for next hit
            comboTimer = 0f;
        }
    }

    void SlamPlayer()
    {
        // Push the player downward after last hit
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb)
        {
            playerRb.linearVelocity = Vector3.down * finalSlamForce;
        }

        // End combo and reactivate NavMesh
        isComboActive = false;
        agent.isStopped = false;
    }
}
