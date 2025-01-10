using UnityEngine;
using System.Collections.Generic;

public class Inventorys : MonoBehaviour
{

    // List to hold references to purchased guns
    [SerializeField] private List<BaseGunScript> _purchasedGuns = new List<BaseGunScript>();

    // The currently equipped gun (if any)
    [SerializeField] private GameObject currentWeapon;
    [SerializeField] private int currentWeaponIndex = 0; // Keep track of the currently equipped weapon index

    [SerializeField] private Transform headTransform;

    // Spawn a new gun based on selection
    public void EquipWeapon(BaseGunScript gunModel)
    {
        // If there's already a weapon equipped, destroy it
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Spawn the new gun
        currentWeapon = Instantiate(gunModel._SpawnGun);
        currentWeapon.transform.SetParent(headTransform);
        currentWeapon.transform.localPosition = gunModel.gunPosition;
        currentWeapon.transform.localRotation = gunModel.gunRotation;
    }

    // Add gun to inventory
    public void AddWeaponToInventory(BaseGunScript newGun)
    {
        if (!_purchasedGuns.Contains(newGun))
        {
            _purchasedGuns.Add(newGun);
            Debug.Log("Weapon added to inventory: " + newGun._GunName);
        }
        else
        {
            Debug.LogWarning("Weapon already in inventory.");
        }
    }

    // Get all purchased weapons
    public List<BaseGunScript> GetInventoryWeapons()
    {
        return _purchasedGuns;
    }

    // Switch to the next weapon in the inventory
    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < _purchasedGuns.Count)
        {
            EquipWeapon(_purchasedGuns[index]);
            currentWeaponIndex = index; // Update the current weapon index
        }
    }

    // Switch to the next or previous weapon based on the scroll wheel input
    void Update()
    {
        // Make sure there are weapons in the inventory
        if (_purchasedGuns.Count == 0)
        {
            Debug.LogWarning("No weapons in the inventory to switch.");
            return; // Exit if no weapons are available
        }

        // Get scroll wheel input (positive = scroll up, negative = scroll down)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f) // Scroll up: Next weapon
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % _purchasedGuns.Count; // Ensure we loop back to the first weapon
            SwitchWeapon(currentWeaponIndex);
        }
        else if (scrollInput < 0f) // Scroll down: Previous weapon
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + _purchasedGuns.Count) % _purchasedGuns.Count; // Loop around if we reach the start
            SwitchWeapon(currentWeaponIndex);
        }
    }

}
