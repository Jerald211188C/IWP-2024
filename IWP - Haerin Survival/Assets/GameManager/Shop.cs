using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript glock;
    [SerializeField] private BaseGunScript RPG;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Gamemanager gamemanager;
    [Header("References")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Button buyAKButton; // Reference to the button in the UI
    private GameObject spawnedGun;

    public void SpawnGun()
    {
        // Spawn the gun and position it
        spawnedGun = Instantiate(glock._SpawnGun);
        spawnedGun.transform.SetParent(weaponHolder);
        spawnedGun.transform.localPosition = glock.gunPosition;
        spawnedGun.transform.localRotation = glock.gunRotation;

        // Add the spawned gun to the inventory
        //inventory.AddWeaponToInventory(gunModel); // Add the gun to the inventory
        Debug.Log("Starting gun added to inventory: " + glock._GunName);
    }

    
}
