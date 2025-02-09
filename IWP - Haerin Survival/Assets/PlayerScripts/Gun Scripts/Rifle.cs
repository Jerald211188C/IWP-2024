using UnityEngine;
using System.Collections;
using TMPro;

public class Rifle : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript _Rifle;
    [SerializeField] private CameraRecoil _cameraRecoil;
    [SerializeField] private PlayerStats _playerStats;

    [Header("Gun Variables")]
    public int CurrentAmmo;
    public int MaxAmmo;
    public int ReserveAmmo;
    public int RPGReserveAmmo;
    public float LastShot;
    public float NextFireTime;
    public float ReloadTime = 2f;
    public int Damage;

    [Header("Gun Prefabs")]
    [SerializeField] private GameObject firePointPrefab;
    [SerializeField] private GameObject _rpgProjectilePrefab;
    [SerializeField] private GameObject _bulletTrailPrefab;

    [Header("Gun UI")]
    private TextMeshProUGUI ammoText;

    private GameObject FirePoint;

    void Start()
    {
        InitializeComponents();
        InitializeGunProperties();
    }

    void Update()
    {
        HandleInput();
        UpdateCooldown();
    }

    private void InitializeComponents()
    {
        if (ammoText == null)
        {
            ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        }

        if (firePointPrefab != null && FirePoint == null)
        {
            FirePoint = Instantiate(firePointPrefab, transform);
            FirePoint.transform.localPosition = _Rifle.firePointPos;
            FirePoint.transform.localRotation = _Rifle.firePointRotation;
        }
    }

    private void InitializeGunProperties()
    {
        CurrentAmmo = _Rifle._MaxAmmo;
        MaxAmmo = _Rifle._MaxAmmo;
        NextFireTime = _Rifle._NextFireTime;
        _bulletTrailPrefab = _Rifle.bulletTrailPrefab;

        UpdateAmmoUI();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && LastShot <= 0f && CurrentAmmo > 0)
        {
            if (_Rifle._isRPG)
            {
                ShootRPG();
            }
            else
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        if (FirePoint == null) return;

        CurrentAmmo--;
        SoundManager.instance.PlaySound(_Rifle._GunShot);
        LastShot = NextFireTime;

        HandleRaycast();
        _cameraRecoil.RecoilFire();
        UpdateAmmoUI();
    }

    private void HandleRaycast()
    {
        Ray ray = new Ray(FirePoint.transform.position, FirePoint.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            HandleEnemyHit(hit);
            SpawnBulletTrail(hit.point);
        }
    }

    private void HandleEnemyHit(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            Health enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                float totalDamage = Damage * _playerStats._DamageMultiplier;
                SoundManager.instance.PlaySound(_Rifle._Hit);
                enemyHealth.Damage(totalDamage);
                Debug.Log($"Damage: {totalDamage}");
            }
        }
    }

    private void SpawnBulletTrail(Vector3 target)
    {
        if (_bulletTrailPrefab == null) return;

        GameObject trail = Instantiate(_bulletTrailPrefab, FirePoint.transform.position, Quaternion.identity);
        StartCoroutine(MoveTrail(trail, target));
        Destroy(trail, 0.5f);
    }

    private IEnumerator MoveTrail(GameObject trail, Vector3 target)
    {
        float elapsedTime = 0f;
        float duration = 0.1f;
        Vector3 start = trail.transform.position;

        while (elapsedTime < duration)
        {
            trail.transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = target;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(ReloadTime);

        if (_Rifle._ispistol)
        {
            CurrentAmmo = 30; // Infinite ammo for pistol
        }
        else if (_Rifle._isRPG)
        {
            int ammoNeeded = MaxAmmo - CurrentAmmo;
            int ammoToLoad = Mathf.Min(ammoNeeded, RPGReserveAmmo);

            CurrentAmmo += ammoToLoad;
            RPGReserveAmmo -= ammoToLoad; // Use RPGReserveAmmo for RPG
        }
        else
        {
            int ammoNeeded = MaxAmmo - CurrentAmmo;
            int ammoToLoad = Mathf.Min(ammoNeeded, ReserveAmmo);

            CurrentAmmo += ammoToLoad;
            ReserveAmmo -= ammoToLoad; // Use ReserveAmmo for regular weapons
        }

        UpdateAmmoUI();
    }

    private void ShootRPG()
    {
        if (FirePoint == null) return;

        GameObject rpgProjectile = Instantiate(_rpgProjectilePrefab, FirePoint.transform.position, FirePoint.transform.rotation);
        Rigidbody rb = rpgProjectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(FirePoint.transform.forward * 50f, ForceMode.Impulse);
        }

        SoundManager.instance.PlaySound(_Rifle._GunShot);
        CurrentAmmo--;
        UpdateAmmoUI();
    }

    private void UpdateCooldown()
    {
        if (LastShot > 0)
        {
            LastShot -= Time.deltaTime;
        }
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            if (_Rifle._ispistol)
            {
                // Display pistol ammo (infinite)
                ammoText.text = $"Ammo: {CurrentAmmo}/{MaxAmmo}";
            }
            else if (_Rifle._isRPG)
            {
                // Display RPG ammo (current and reserve)
                ammoText.text = $"Ammo: {CurrentAmmo}/{RPGReserveAmmo}";
            }
            else
            {
                // Display regular rifle ammo (current and reserve)
                ammoText.text = $"Ammo: {CurrentAmmo}/{ReserveAmmo}";
            }
        }
    }
}