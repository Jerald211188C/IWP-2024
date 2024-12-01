using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Gun References")]
    [SerializeField] private BaseGunScript glock;
    [SerializeField] private BaseGunScript RPG;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Gamemanager gamemanager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // No need to assign spawnedGun here, we can directly use glock in the spawn method
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            // Add the gun to the inventory when the player presses "U"
            gamemanager.AddWeaponToInventory(glock);
            Debug.Log("Glock added to inventory");

            // Optionally, immediately equip the weapon after purchasing
            gamemanager.EquipWeapon(glock);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            // Add the gun to the inventory when the player presses "U"
            gamemanager.AddWeaponToInventory(RPG);
            Debug.Log("Glock added to inventory");

            // Optionally, immediately equip the weapon after purchasing
            gamemanager.EquipWeapon(RPG);
        }
    }
}
