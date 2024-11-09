using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Rifle : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript _Rifle;

    [Header("Gun Variables")]
    public string GunName;
    public int CurrentAmmo;
    public int MaxAmmo;
    public int Damage;
    public float LastShot;
    public float NextFireTime;
    public float ReloadTime = 2f;
    public GameObject FirePoint;
    [SerializeField] private GameObject firePointPrefab;  // Reference to the FirePoint prefab

    [Header("Gun UI")]
    private TextMeshProUGUI ammoText;



    void Start()
    {
        if (ammoText == null)
        {
            ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();  // "AmmoText" is the name of the UI Text object
        }

        if (firePointPrefab != null)
        {
            // Instantiate the FirePoint prefab and assign it to FirePoint
            FirePoint = Instantiate(firePointPrefab, transform);  // This makes FirePoint a child of the gun
            FirePoint.transform.localPosition = _Rifle.firePointPos; // Position it at (0, 0, 3.67) relative to the gun's origin

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
            Gamemanager._instance.IsPlayerWalking();
            Debug.Log(CurrentAmmo);
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
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f); // Length of the ray is 100 units
            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        // Optionally play a reload animation here

        yield return new WaitForSeconds(ReloadTime); // Wait for the reload duration

        CurrentAmmo = MaxAmmo; // Reset ammo after reloading
        Debug.Log("Reload complete!");
        // Optionally stop the reload animation here
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + CurrentAmmo + "/" + MaxAmmo;
    }

}
