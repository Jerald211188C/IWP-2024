using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Rifle : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript _Rifle;
    [SerializeField] private GameObject _enemyObject; // Make sure to assign this in the Inspector
    [SerializeField] private CameraRecoil _cameraRecoil;  
    private SkillTree _skillTree;
    
    private SimpleEnemy _Enemy;
    [Header("Gun Variables")]
    public string GunName;
    public int CurrentAmmo;
    public int MaxAmmo;
    public int Damage;
    public float LastShot;
    public float NextFireTime;
    public float ReloadTime = 2f;
    private GameObject FirePoint;
    [SerializeField] private GameObject firePointPrefab;  // Reference to the FirePoint prefab
    [Header("Gun UI")]
    private TextMeshProUGUI ammoText;

    private void Awake()
    {
        _skillTree = new SkillTree();
    }
    void Start()
    {

        //Enemy = _enemyObject.GetComponent<SimpleEnemy>();
        if (ammoText == null)
        {
            ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        }

        if (firePointPrefab != null)
        {
            // Instantiate the FirePoint prefab and assign it to FirePoint
            FirePoint = Instantiate(firePointPrefab, transform);  // This makes FirePoint a child of the gun
            FirePoint.transform.localPosition = _Rifle.firePointPos; // Position it at (0, 0, 3.67) relative to the gun's origin
            FirePoint.transform.localRotation = _Rifle.firePointRotation;

        }

        CurrentAmmo = _Rifle._CurrentAmmo;
        MaxAmmo = _Rifle._MaxAmmo;
        CurrentAmmo = MaxAmmo;
        GunName = _Rifle._GunName;
        Damage = _Rifle._Damage;
        LastShot = _Rifle._LastShot;
        NextFireTime = _Rifle._NextFireTime;

        LastShot = NextFireTime;
        
        Gamemanager._instance._iswalking += Shoot;
        //GetFirePoint(); 
        UpdateAmmoUI();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && LastShot <= 0f && CurrentAmmo > 0)
        {
            CurrentAmmo -= 1;
            Shoot();
            //Gamemanager._instance.IsPlayerWalking();
            //Debug.Log(CurrentAmmo);
            ammoText.text = CurrentAmmo.ToString();
            LastShot = NextFireTime;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
          
        }

        LastShot -= Time.deltaTime;
        UpdateAmmoUI();
    }

    private void Shoot()
    {
   
        if (FirePoint != null)
        {
            Ray ray = new Ray(FirePoint.transform.position, FirePoint.transform.forward);
            RaycastHit hit;

            // Visualize the ray in the Scene view
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f); // Length of the ray is 100 units
            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Check if the hit object has the "Enemy" tag
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Hit an enemy: " + hit.collider.name);

                    // Get the Health component from the hit enemy
                    Health enemyHealth = hit.collider.GetComponent<Health>();

                    if (enemyHealth != null)
                    {
                        // Apply damage to the enemy
                        enemyHealth.Damage(Damage); // 10 is the damage value (you can change this)
                    }
                    else
                    {
                        Debug.LogWarning("No Health component found on the enemy.");
                    }
                }
                else
                {
                    //Debug.Log("Hit: " + hit.collider.name);
                }
            }
        }

        _cameraRecoil.RecoilFire();
    }


    private IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        // Optionally play a reload animation here

        yield return new WaitForSeconds(0.1f); // Wait for the reload duration

        CurrentAmmo = MaxAmmo; // Reset ammo after reloading
        Debug.Log("Reload complete!");
        // Optionally stop the reload animation here
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + CurrentAmmo + "/" + MaxAmmo;
    }

    public bool DealMoreDamage()
    {
        return _skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreDamage);
    }

    public SkillTree GetPlayerSkills()
    {
        return _skillTree;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
        Debug.Log("Damage increased by: " + amount + ". New Damage: " + Damage);
    }

}
