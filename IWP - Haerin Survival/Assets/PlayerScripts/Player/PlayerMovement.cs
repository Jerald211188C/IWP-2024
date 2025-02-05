using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Controller References")]
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private Transform _PlayerCamera;
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _Player;
    private bool isKnockbackActive = false;
    [Header("Player Variables")]
    [SerializeField] private float _Speed; // Base speed
    [SerializeField] private float _SprintMultiplier = 1.5f; // Sprint speed multiplier
    [SerializeField] private float _JumpForce;
    [SerializeField] private float _Gravity = -9.81f;
    [SerializeField] private PlayerStats _Stats;
    [SerializeField] private GameObject _SkillTreeUI;
    [SerializeField] private GameObject ShopUI;
    private bool isPlayerInShop;

    [Header("Knockback Settings")]
    private Vector3 knockbackVelocity; // Store knockback velocity here
    private float knockbackDuration = 0.1f; // Duration of the knockback effect

    [Header("Stats UI")]
    [SerializeField] private TextMeshProUGUI _CoinsTXT;
    public int EXP;
    public int _Coins;
    public int _CoinToAdd;
    private Health health;
    public Health Health => health;
    private LevelUpSystem _levelUpSystem;

    private Vector3 _Velocity;
    private Vector3 _PlayerMovementInput;
    private Vector2 _PlayerMouseInput;

    private void Awake()
    {
        isPlayerInShop = false;
        // Ensure the ShopUI is disabled at the start
        if (ShopUI != null)
        {
            ShopUI.SetActive(false);
        }
        if (_SkillTreeUI != null)
        {
            // Temporarily enable the UI
            _SkillTreeUI.SetActive(true);
            // Disable the UI again
            _SkillTreeUI.SetActive(false);
        }
        health = GetComponent<Health>();
        health.Death.AddListener(Death);
    }


void Update()
    {

        if (isKnockbackActive)
        {
            // Apply knockback and simulate gravity (arc-like motion)
            _CharacterController.Move(knockbackVelocity * Time.deltaTime);
            knockbackVelocity.y += _Gravity * Time.deltaTime; // Apply gravity to simulate arc

            if (knockbackVelocity.y <= 0 && _CharacterController.isGrounded)
            {
                // End knockback when the player hits the ground
                isKnockbackActive = false;
            }

            return;
        }

        _PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        RotatePlayer();
        UpdateCoinsUI();
        HandleSkillTreeToggle();
    }

    private void MovePlayer()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Get the camera's forward and right direction
        Vector3 forward = _PlayerCamera.transform.forward;
        Vector3 right = _PlayerCamera.transform.right;
 
        // Flatten the vectors to ignore the y-component
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors
        forward.Normalize();
        right.Normalize();

        // Calculate the desired move direction
        Vector3 moveDirection = (forward * _PlayerMovementInput.z + right * _PlayerMovementInput.x).normalized;

        // Set speed based on whether sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) ? _Speed * _SprintMultiplier : _Speed;

        if (_CharacterController.isGrounded)
        {
            _Velocity.y = -1f; // Reset the downward velocity when grounded

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _Velocity.y = _JumpForce;
            }
        }
        else
        {
            _Velocity.y += _Gravity * Time.deltaTime; // Apply gravity
        }

        // Move the character controller
        Vector3 move = transform.right * x + transform.forward * z;
        _CharacterController.Move(move * currentSpeed * Time.deltaTime);
        //_CharacterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        _CharacterController.Move(_Velocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        // Only apply the yaw (horizontal rotation) from the camera to the player
        float playerYaw = _Camera.transform.eulerAngles.y;
        _Player.transform.rotation = Quaternion.Euler(0f, playerYaw, 0f);
    }

    private void Testing()
    {
        Debug.Log("I HAERIN");
    }

    private void OnDestroy()
    {
        if (Gamemanager._instance != null)
        {
            Gamemanager._instance._iswalking -= Testing; // Unsubscribe from the event
        }
    }

    private void Death()
    {
        //DO other req stuff to kill player like pause game, do ani and return to main menu
        Destroy(gameObject);
        Debug.Log("Player has died.");
    }

    private void ToggleSkillTree()
    {
        if (_SkillTreeUI != null)
        {
            bool isSkillTreeActive = !_SkillTreeUI.activeSelf; // Determine if the skill tree is being shown or hidden

            _SkillTreeUI.SetActive(isSkillTreeActive); // Toggle the skill tree UI

            if (isSkillTreeActive)
            {
                // When the skill tree is shown, unlock the cursor and make it visible
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // When the skill tree is hidden, lock the cursor and make it invisible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }


    private void HandleSkillTreeToggle()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSkillTree();
        }
    }


    public void IncreaseEXPGain(int _MoreExp)
    {
       EXP += _MoreExp;
    }

    public void IncreaseCoinGain(int _MoreCoins)
    {
        _CoinToAdd += _MoreCoins;
    }

    public void UpdateCoinsUI()
    {
        _CoinsTXT.text = " " + _Stats._StartingCoins.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the shop's collider
        if (other.CompareTag("Shop"))
        {
            isPlayerInShop = true;

            // Enable the shop UI
            if (ShopUI != null)
            {
                ShopUI.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Player entered the shop.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the shop's collider
        if (other.CompareTag("Shop"))
        {
            isPlayerInShop = false;

            // Disable the shop UI
            if (ShopUI != null)
            {
                ShopUI.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            Debug.Log("Player left the shop.");
        }
    }

    public void ApplyKnockback(Vector3 knockbackDirection, float knockbackForce)
    {
        // Apply an initial velocity in the direction of the knockback
        knockbackVelocity = knockbackDirection * knockbackForce;
        knockbackVelocity.y = 5f; // Add a small initial upward force to simulate an arc
        isKnockbackActive = true;
    }

}
