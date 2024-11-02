using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript _Rifle;
    [SerializeField] private Camera _camera; // Ensure this is assigned in the Inspector

    [Header("Gun Variables")]
    public string GunName;
    public int CurrentAmmo;
    public int MaxAmmo;
    public int Damage;
    public int ReloadTime;
    public float FireRate;

    public GameObject GunModel;
    public GameObject FirePoint;

    void Start()
    {
        GunName = _Rifle._GunName;
        CurrentAmmo = _Rifle._CurrentAmmo;
        MaxAmmo = _Rifle._MaxAmmo;
        Damage = _Rifle._Damage;
        ReloadTime = _Rifle._ReloadTime;
        FireRate = _Rifle._FireRate;
        //GunModel = _Rifle._GunModel;

        CurrentAmmo = MaxAmmo;
        Gamemanager._instance._iswalking += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun(); // Call to rotate the gun

        if (Input.GetMouseButton(0))
        {
            Gamemanager._instance.IsPlayerWalking();
        }
    }

    private void RotateGun()
    {
        // Get the camera's rotation
        Quaternion cameraRotation = _camera.transform.rotation;

        // Create a new rotation using the camera's X and Y values, with Z fixed at 90 degrees
        Quaternion gunRotation = Quaternion.Euler(cameraRotation.eulerAngles.x, cameraRotation.eulerAngles.y, 90f);

        // Set the gun's rotation
        GunModel.transform.rotation = gunRotation;
    }


    private void Shoot()
    {
        // Create a ray from the fire point
        Ray ray = new Ray(FirePoint.transform.position, FirePoint.transform.forward);
        RaycastHit hit;

        // Visualize the ray in the Scene view
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f); // Length of the ray is 100 units

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit: " + hit.collider.name);
            // Handle hit (e.g., apply damage)
        }
    }
}
