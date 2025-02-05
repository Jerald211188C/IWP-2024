using UnityEngine;
using System;

public class Gamemanager : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript gunModel; // The gun model for the starting gun
    [SerializeField] private Transform headTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject Head;
    [SerializeField] private PlayerStats playerStats;
    private GameObject spawnedGun;
    private Inventorys inventory;

    [Header("Event System")]
    public static Gamemanager _instance;

    private void Awake()
    {
        playerStats.ResetStats();
        _instance = this;
    }

    public void EquipWeapon(BaseGunScript gunModel)
    {
        SpawnGun();
    }

    public event Action _iswalking;

    // Event for walking
    public void IsPlayerWalking()
    {
        _iswalking?.Invoke();
    }

    // Spawn the starting gun and add it to the inventory
    public void SpawnGun()
    {
        // Spawn the gun and position it
        spawnedGun = Instantiate(gunModel._SpawnGun);
        spawnedGun.transform.SetParent(headTransform);
        spawnedGun.transform.localPosition = gunModel.gunPosition;
        spawnedGun.transform.localRotation = gunModel.gunRotation;

        // Add the spawned gun to the inventory
        //inventory.AddWeaponToInventory(gunModel); // Add the gun to the inventory
        Debug.Log("Starting gun added to inventory: " + gunModel._GunName);
    }

    // Add weapon to inventory

    // Spawn additional guns (can be used in the shop, for example)
    public void SpawnGuns(BaseGunScript _gunModel, Transform _headTrans)
    {
        if (_gunModel._SpawnGun == null)
        {
            Debug.LogError("Gun prefab not assigned in BaseGunScript!");
            return;
        }

        GameObject _gun = Instantiate(_gunModel._SpawnGun);
        _gun.transform.SetParent(_headTrans);
        _gun.transform.localPosition = _gunModel.gunPosition;
        _gun.transform.localRotation = _gunModel.gunRotation;
    }
}
